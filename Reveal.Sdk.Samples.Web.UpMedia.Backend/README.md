# Sample application using [RevealBI](https://revealbi.io/) AspNetCore SDK
[![Nuget](https://img.shields.io/nuget/v/Reveal.Sdk.Web.AspNetCore.Trial)](https://www.nuget.org/packages/Reveal.Sdk.Web.AspNetCore.Trial/)
#### [Website](https://revealbi.io/) | [Docs](https://help.revealbi.io/en/developer/web-sdk/overview.html) | [API Reference](https://help.revealbi.io/api/aspnet/latest/Reveal.Sdk.html)

ℹ️ | This project provides only the backend piece, you can use it along [upmedia-react](https://github.com/RevealBi/sdk-samples-react/tree/main/upmedia-react) or [upmedia-browser](https://github.com/RevealBi/sdk-samples-react/tree/main/upmedia-browser) for the frontend.
:---: | :---
## Usage
Download the code and run the Reveal.Sdk.Samples.Web.UpMedia.Backend.csproj project.

This will run the server at port 8080, you can verify the server is working properly by accessing http://localhost:8080/upmedia-backend/reveal-api/DashboardFile/Sales that will return the JSON document for the Sales sample dashboard.


## How it works

1. Defines a dashboards service ([IDashboardService](Services/IDashboardService.cs) and [DashboardService](Services/DashboardService.cs)). It reads all of the .rdash files that were added to the project as embedded resources([Dahsboards](Dashboards) folder) and initializes it's dictionary on top of them. 
Register the this service in AspNetCore DI by calling the following snippet in Startup's Configure Services method:
```c#
services.AddSingleton<IDashboardService, DashboardService>();
```
2. Creates the [DashboardProvider](SDK/DashboardProvider.cs) class. It accepts an IDashboardService argument in it's constructor so could get the service implementation injected. Override the GetDashboardAsync abstract method on top of the DashboardService. Finally add reveal services and register DashboardProvider by doing the following call:
```c#
services
    .AddControllers()
    .AddReveal(builder => builder.AddDashboardProvider<DashboardProvider>());
```
3. Creates a [DashboardsController](Controllers/DashboardsController.cs), which also receives the [IDashboardService](Services/IDashboardService.cs). Mark it with [Route("Dashboards")] to define the route(this is the route [upmedia-react](https://github.com/RevealBi/sdk-samples-react/tree/main/upmedia-react) client sample will be hitting but you could specify a path that suits your needs). Add a method that gets all the available dashboards from the IDashboardService extract DashboardInfo objects out of the Dashboard instances and return them. Mark that method with [HttpGet] attribute.
