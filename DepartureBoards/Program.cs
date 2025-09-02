using DepartureBoards.Components;
using DepartureBoards.Data;
using MudBlazor.Services;
using System.Globalization;

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

            //builder.Services.AddMudExtensions();
            //builder.Services.AddMudServicesWithExtensions();
            //builder.Services.AddLocalization();

            //CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("cs-CZ");
            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("cs-CZ");

            //builder.Services.AddLocalization(options =>
            //{
            //    options.ResourcesPath = "Resources";
            //});

            builder.Services.AddLocalization();
            builder.Services.AddSingleton<DataHandler, FileHandler>();
            builder.Services.AddSingleton<ApiHandler>();

            var app = builder.Build();

            var supportedCultures = new[] { "cs-CZ", "en-US" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            app.UseRequestLocalization(localizationOptions);
            
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
