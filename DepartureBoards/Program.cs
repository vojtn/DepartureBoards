using DepartureBoards.Components;
using DepartureBoards.Data;
using DepartureBoards.Services;
using MudBlazor.Services;
using System.Globalization;
using BitzArt.Blazor.Cookies;


namespace DepartureBoards
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient();
            builder.Services.AddServerSideBlazor()
                .AddCircuitOptions(options => { options.DetailedErrors = true; });
            builder.Services.AddMudServices();

            builder.Services.AddSingleton<ApiHandler>();

            // Setup database
            builder.Services.AddScoped<UserService>();

            // Support cookies
            builder.AddBlazorCookies();

            // cookie for dark mode
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ThemeService>();

            // Support localization
            builder.Services.AddLocalization();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Localization
            var supportedCultures = new[] { "cs-CZ", "en-US" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);
            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
