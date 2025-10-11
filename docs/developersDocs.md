# Conceptual model 
From [user stories](/docs/requirements.md) above we analysed the need of the main actor which is the user. The user can have multiple favorite Stops. Each stop has a name and a list of Platforms, typically one platform for each direction, where vehicles go to.  

![](/docs/conceptualModel.png)

## Models
In the conceptual model we presented the classes we need; in the code they are represented by the `User` and `Board` classes, implemented in `DepartureBoards/Data/Model.cs`.

```csharp
public class User
{
    [Key]
    public string Name { get; set; } = String.Empty; // Unique identifier for the user
    public List<Board>? Boards { get; set; } // List of favorite boards for the user

    public User(string Name) 
    {
        this.Name = Name;
    }
}

public class Board
{
    public int Id { get; set; } // Unique identifier for the board
    public string Name { get; set; } = String.Empty; // Name of the stop
    public int Order { get; set; } // Order for sorting boards
    public List<string> Platforms { get; set; } = new(); // List of platforms at the stop
    public string UserName { get; set; } // Foreign key to User
    public User User { get; set; } = null!; // Reference navigation property
}
```


## Architecture
The frontend is created with Razor pages. Fetching APIs is handled by the `ApiService` class, which accesses the PID open API (see https://pid.cz/en/opendata/).  


Data about individual users and their favorite stops are stored in a Azure SQL database using Entity Framework core. User data is managed by the `UserService` class.  

![](/docs/arch.png)





## Frontend

The frontend is built using Blazor Server and Razor components, with [MudBlazor](https://mudblazor.com/) providing UI elements for a modern look and responsive design.

### Razor Components

#### AppMenu
[`Component.AppMenu`](/DepartureBoards/Components/Component/AppMenu.razor) is the main navigation bar, present on all pages via the MainLayout. It provides:
- Navigation to the homepage
- Dark mode toggle (using [`ThemeService`](/DepartureBoards/Services/ThemeService.cs))
- Language selection
- Link to the project's GitHub repository

On first render, it checks for a dark mode cookie and sets the theme accordingly.

#### BoardForm
[`Component.BoardForm`](/DepartureBoards/Components/Component/BoardForm.razor) is used for both adding and editing boards. It provides a form to select a stop and platforms, using autocomplete and other MudBlazor controls.

#### BoardCard
[`Component.BoardCard`](/DepartureBoards/Components/Component/BoardCard.razor) displays summary information for a board, uses SingleBoard or SharedBoard based on user settings to show departures from all platforms or just selected ones. 

#### SingleBoard
[`Component.SingleBoard`](/DepartureBoards/Components/Component/SingleBoard.razor) shows departures for a single platform at a stop.

#### SharedBoard
[`Component.SharedBoard`](/DepartureBoards/Components/Component/SharedBoard.razor) displays departures for multiple platforms at the same stop, using MudBlazor's DataGrid for tabular display.

### Pages

#### Homepage
[`Pages.Home`](/DepartureBoards/Components/Pages/Home.razor) is the landing page, showing project info and navigation options for creating a user or accessing an existing account.

#### CreateUser page
[`Pages.CreateUser`](/DepartureBoards/Components/Pages/CreateUser.razor)
Allows new users to register by entering a username.

#### AddBoard page
[`Pages.Boards.AddBoard`](/DepartureBoards/Components/Pages/Boards/AddBoard.razor)
Lets users add a new favorite board by selecting a stop and platforms.

#### EditBoard page
[`Pages.Boards.EditBoard`](/DepartureBoards/Components/Pages/Boards/EditBoard.razor)
Allows users edit his favorite board.

#### Boards page
[`Pages.Boards.Boards`](/DepartureBoards/Components/Pages/Boards/Boards.razor)
Shows all favorite boards for the registered user, with options to edit, reorder, or delete boards.


## Backend

The backend is built with ASP.NET Core and Entity Framework Core, providing data management, API integration, and user preferences.

### Data Access

User and board data are stored in an Azure SQL database. The [`Data.AppDbContext`](DepartureBoards/Data/AppDbContext.cs) class configures the database connection and exposes tables via `DbSet<User>` and `DbSet<Board>`.

```csharp
public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
    }
```

### UserService

The [`Services.UserService`](/DepartureBoards/Services/UserService.cs) class provides CRUD operations for users and boards. It is registered for dependency injection in [`Program.cs`](DepartureBoards/Program.cs):

```csharp
// Setup database
builder.Services.AddScoped<UserService>();
```

Then we implemented UserService with typical CRUD operations for managing users and boards.

### ApiService
[`Services.ApiService`](/DepartureBoards/Services/ApiService.cs) class handles communication with the [PID Open API](https://pid.cz/en/opendata/) using `HttpClient`. Main methods:
- `GetStopsAsync`: Fetches all available stops for use in board forms
- `GetDepartureBoard`: Retrieves departure data for a specific stop and platform


### Theme Service
[`Services.ThemeService`](DepartureBoards/Services/ThemeService.cs) manages dark mode preferences using cookies via [BitzArt/Blazor.Cookie](https://github.com/BitzArt/Blazor.Cookies). It provides methods to get and set the dark mode cookie, and defaults to the device setting if no cookie is found.

Setup in Program.cs
```csharp
    // Support cookies
    builder.AddBlazorCookies();

    // Setup ThemeService
    builder.Services.AddScoped<ThemeService>();
```
When there is not any cookie set, the app uses the device default setting, which can be easily detected using MudThemeProvider and 'GetSystemDarkModeAsync' method, which is handled in the AppMenu.

In our ThemeService we implemented two methods, one for getting DarkMode preference from cookie and the second one for setting it.

```csharp
    /// <summary>
    /// Gets darkmode cookie
    /// </summary>
    /// <returns></returns>
    public async Task<Cookie?> GetDarkModeCookieAsync()
    {
        return await cookieService.GetAsync(DarkModeCookie);
    }
    /// <summary>
    /// Sets darkmode preference in a cookie
    /// </summary>
    /// <param name="isDarkMode"></param>
    /// <returns></returns>
    public async Task SetDarkModeAsync(bool isDarkMode)
    {
        await cookieService.SetAsync(DarkModeCookie, isDarkMode.ToString().ToLowerInvariant());
    }
```

## Localization
Blazor provides a straightforward way to handle localization using **resource (.resx) files** and the built-in localization services in ASP.NET Core.

The initial localized data is stored in `.resx` resource files.  
To enable localization in your app, we needed to register it in `Program.cs`:

```csharp
    // Support localization
    builder.Services.AddLocalization();
    builder.Services.AddControllers();

    var supportedCultures = new[] { "cs-CZ", "en-US" };
    var localizationOptions = new RequestLocalizationOptions()
        .SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
    app.UseRequestLocalization(localizationOptions);
    app.MapControllers();

```

Then in the razor pages it just to inject IStringLocalizer

```csharp
    @inject IStringLocalizer<Resource> Localizer
```
And use it easily 

```csharp
    <MudText>@Localizer["WelcomeMessage"]</MudText>
```

The [CultureController](/DepartureBoards/Controllers/CultureController.cs) is responsible for setting the culture cookie based on the selected language.
Once the cookie is set, Blazor automatically updates the displayed language on the next navigation.

The language switch button is defined in the AppMenu component.
It changes the current culture by calling the CultureController endpoint, which updates the cookie and reloads the app in the selected language.

This allows users to toggle between English and Czech language.