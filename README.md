# DepartureBoards

**DepartureBoards** is a web app that uses the [PID Open API](https://pid.cz/en/opendata/) to display customized public transport departure boards for Prague. Users can select and manage their favorite stops and platforms for quick access.

You can try it live at: https://departureboards.azurewebsites.net

## Motivation

I created this app because the PID Lítačka app doesn't offer an easy way to view a specific departure board - like the physical displays you see at tram or bus stops. This project is a digital version of those real-world boards.

![](/docs/oznacnik.jpeg)

## Utilization of AddToHomeScreen functionality

Google Chrome web browser includes a feature, which allows users to create a shortcut for some webpage and this fact can be very useful, cause it will add Icon to the home screen and it will behave like an app

![](/docs/a2hs.png)
You can read more about it [here](https://support.google.com/chrome/answer/15085120?hl=en&co=GENIE.Platform%3DAndroid)

## Docs
[User requirements](/docs/requirements.md)
[Developer documentation](/docs/developersDocs.md)

## Used Technologies
- ASP.NET (Blazor)
- MudBlazor
- Entity Framework core
- Azure SQL database

### Packages
- MudBlazor 8.9
- Microsoft.EntityFrameworkCore 9.0.9
- Microsoft.Extensions.Localization 9.0.8
- BitzArts.Blazor.Cookies.Server 1.5.0   

## License
See the [license](/LICENSE.md) file for license rights and limitations (MIT).