# Web sample application using [RevealBI](https://revealbi.io/) AspNetCore SDK
[![Nuget](https://img.shields.io/nuget/v/Reveal.Sdk.Web.AspNetCore.Trial)](https://www.nuget.org/packages/Reveal.Sdk.Web.AspNetCore.Trial/)
#### [Website](https://revealbi.io/) | [Docs](https://help.revealbi.io/en/developer/web-sdk/overview.html) | [API Reference](https://help.revealbi.io/api/aspnet/latest/Reveal.Sdk.html)

## Usage
Download the code and run the Cookies-Auth.csproj project.

## What to expect
This application includes a single page that renders a dashboard with two visualizations, which are using a REST service that echoes the list of headers and cookies received, just for testing purposes.

When you open the page at http://localhost:8080/ you should see something like this:

![aspnetcore-cookies-auth-screenshot](https://user-images.githubusercontent.com/7972319/123778995-6130f280-d8da-11eb-808e-375b271e62b9.PNG)


In the first visualization you can see the standard headers sent in the request to the REST data source, like "user-agent" or "accept-encoding", also a "cookie" header including a hard-coded cookie added in the sample code.
The second visualization is pretty similar but displays only parsed cookies, it can be used to confirm the "cookie" header is processed and parsed properly.

## How it works

In this application we want to get data from a REST service, and this service requires a header/cookie for authentication. The header/cookie we need to send is the same cookie used for authentication in the application. So, basically we need to "forward" a header/cookie from the application to the data source.

To keep things simple for this sample, we'll use a sample "samplesessionheader" as the header to forward, also adding a "testCookie1" to be passed to the request to the data source. 

### Storing the value for a header in the user context
In [SampleUserContextProvider](Reveal/SampleUserContextProvider.cs) you can see we're creating a sample header and cookie. In your application you might need to use a cookie or an authentication header.

_SampleUserContextProvider_ implements _IRVUserContextProvider_ interface and defines the _GetUserContext_ method that returns a _RVUserContext_ object, which contains both the userId and a set of properties.
We're using this list of properties to store the samplesessionheader, this way we can retrieve it later when credentials for a data source are requested.

```c#
internal class SampleUserContextProvider : IRVUserContextProvider
{
    public IRVUserContext GetUserContext(HttpContext aspnetContext)
    {
        //we don't have a real authentication in this sample, so we're just hard-coding "guest" as the user here
        //when using standard auth mechanisms, the userId can be obtained using something like aspnetContext.User.Identity.Name.
        string userId = "guest";

        //RVUserContext is a default implementation of IRVUserContext, which allows to store properties in addition to the userId, these properties can be used later
        //for example in the authentication provider. You could store data related to the current request this way. In this case, we're just storing a sample header data.
        return new RVUserContext(
            userId,
            new Dictionary<string, object>() { { "samplesessionheader", "sampleSessionHeaderValue" } });
    }
}
```

### Retrieving the header value from the user context
In [SampleAuthenticationProvider](Reveal/SampleAuthenticationProvider.cs) we're implementing _IRVAuthenticationProvider_ interface, which takes care of passing a _RVUserContext_ to _ResolveCredentialsAsync_.
We're using that _RVUserContext_ object to get the "samplesessionheader" property we installed in _SampleUserContextProvider_.

In this case our data source is a REST API data source, and we want to pass some header/cookies as the credentials for it, including the samplesessionheader header, so we need to return a _RVHeadersDataSourceCredentials_ object. This class can be used to pass authentication credentials object only for REST or Web Resource data sources.

As _RVHeadersDataSourceCredentials_ is constructed with one or more headers, we want to create the "Cookie" header to be sent as well. Here we're adding the samplesessionheader to the request along with a test cookie.

```c#
internal class SampleAuthenticationProvider : IRVAuthenticationProvider
{
    public Task<IRVDataSourceCredential> ResolveCredentialsAsync(IRVUserContext userContext, RVDashboardDataSource dataSource)
    {
        if (dataSource is RVRESTDataSource)
        {
            //get the session sampleSessionHeader value
            string sessionHeader = (string)userContext.Properties.GetValueOrDefault("samplesessionheader");

            //pass a fixed cookie just for testing purposes and the sessionSesionHeader we stored in the SampleUserContextProvider
            string cookies = $"testCookie1=testValue";

            return Task.FromResult<IRVDataSourceCredential>(new RVHeadersDataSourceCredentials(
                new Dictionary<string, string>()
                {
                    { "userId", userContext.UserId },
                    { "cookie", cookies},
                    { "sampleSessionHeader", sessionHeader}
                }));
        }
        return null;
    }
}
```
