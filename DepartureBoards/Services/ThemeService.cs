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
        private const string DarkModeCookie = "DarkMode";
        public MudTheme Theme { get; set; } = new MudTheme()
        {
            PaletteLight = new PaletteLight()
            {
                Primary = "#02adadff",//"#008C95",
            },
            PaletteDark = new PaletteDark()
            {
                Primary = "#02adadff"//"#1ea3adff",
                //Primary = Colors.Green.Darken1,
                //Secondary = Colors.Orange.Default,
                //Background = Colors.Shades.Black
            }
        };
        public ThemeService(ICookieService CookieService)
        {
            cookieService = CookieService;
        }
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
    }
}
