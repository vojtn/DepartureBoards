using DepartureBoards.Components;
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

            var app = builder.Build();
            app.UseRequestLocalization(new RequestLocalizationOptions()
                .AddSupportedCultures(new[] { "en-US", "cs-CZ" })
                .AddSupportedUICultures(new[] { "en-US", "cs-CZ" }));

            //builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            var supportedCultures = new[] { "cs-CZ" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs-CZ");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs-CZ");

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
