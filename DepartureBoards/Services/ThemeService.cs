using BitzArt.Blazor.Cookies;
using Microsoft.JSInterop;
using MudBlazor;

namespace DepartureBoards.Services
{
    /// <summary>
    /// Handles users preferences like darkmode setting via a cookie to keep persistant state across sessions
    /// </summary>
    public class ThemeService
    {
        private readonly ICookieService cookieService;
        private readonly IJSRuntime jsRuntime;
        private const string DarkModeCookie = "DarkMode";
        public MudTheme Theme { get; set; } = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#02adadff",//"#008C95",
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#02adadff"//"#008C95",
                //Primary = Colors.Green.Darken1,
                //Secondary = Colors.Orange.Default,
                //Background = Colors.Shades.Black
            }
        };
        public ThemeService(ICookieService CookieService, IJSRuntime JSRuntime)
        {
            cookieService = CookieService;
            jsRuntime = JSRuntime;
        }
        /// <summary>
        /// Gets darkmode setting from cookie
        /// If there the cookies is not set, it takes the system settings for darkmode
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetDarkModeAsync()
        {
            /// default OS setting
            var cookie = await cookieService.GetAsync(DarkModeCookie);
            if (cookie is null)
            {
                return await jsRuntime.InvokeAsync<bool>("prefersDarkMode");
            }
            return bool.TryParse(cookie.Value, out var result) && result;
        }
        /// <summary>
        /// Set darkmode setting in a cookie
        /// </summary>
        /// <param name="isDarkMode"></param>
        /// <returns></returns>
        public async Task SetDarkModeAsync(bool isDarkMode)
        {
            await cookieService.SetAsync(DarkModeCookie, isDarkMode.ToString().ToLowerInvariant());
        }
    }
}
