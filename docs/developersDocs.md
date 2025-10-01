
## Functional requirements (user stories)
1. As a user, I want to view a page showing an overview of all my favorite departure boards, so I can quickly access my preferred stops.
2. As a user, I want to add a new departure board to my favorites, so I can monitor departures from important stops.
3. As a user, I want to choose specific platforms of a stop when adding a departure board, so I only see the departures I care about.
4. As a user, I want to remove a departure board from my favorites, so I can keep my list relevant and up to date.
5. As a user, I want to edit a favorite departure board and its selected platforms, so I can adjust them as needed.

## Conceptual model 
From user stories above we analysed the need of the main actor which is user. The user can have multiple favorite Stops. Each stop has a name and list of Platforms, typically one platform for each direction, where vehicles go to. 
![](/docs/conceptualModel.png)

## Architecture
Frontend is created with of razor pages. Fetching APIs are done by ApiHandler class, which taks is to access PID open API, see https://pid.cz/en/opendata/.
Data about individual users and their favorite stops are stored in a json file, access to the file is managed by DataHandler class.
![](/docs/arch.png)

### DataHandler
This is an abstract class, which main resposibility is to manage access to data about users and theirs favorite stops. It handles methods like TryAddUser, GetUser, DeleteBoard and EditBoard.
DataHandler is implemented by ApiHandler class, which uses data.json file to stored users data.

### ApiHandler
As already mentioned above, the main goal of this class is to fetch PID APIs, in .NET we used HttpClient.
It used 2 main methods **GetStopsAsync** to get list of all available stops, which is used in Add page.
And **GetDepartureBoard** for data about individual boards, which is the most important thing about this app, to display board from the given stop.

### Other Classes
In the conceptual model we presented the classed we need, in the code they are represented by User, StopInfo and Users classes.

## FrontEnd
This app uses MudBlazor library for come UI components, this helps us to save time.

### Pages
#### Homepage
Basic info 

#### Create user page

#### Add board page

#### User favorite page

#### Edit page
### Components
#### Board
The main board displaying stop departure is a custom razor component, which uses MudDataGrid under the hood,
#### Menu
For application menu we used Mudblazor AppBar. 

## Localization
Blazor provides an easy was for handling localization