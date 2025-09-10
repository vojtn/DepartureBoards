namespace PreferenceTest.Controllers
{
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller to manage culture settings.
    /// SOURCE: https://learn.microsoft.com/en-us/aspnet/core/blazor/globalization-localization?view=aspnetcore-9.0#server-project-updates
    /// </summary>
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        public IActionResult Set(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture, culture)));
            }
            return LocalRedirect(redirectUri);
        }
    }
}
