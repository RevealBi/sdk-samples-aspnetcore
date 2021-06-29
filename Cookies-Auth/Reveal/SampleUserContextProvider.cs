using Microsoft.AspNetCore.Http;
using Reveal.Sdk;
using Reveal.Sdk.AspNetCore;
using System.Collections.Generic;

namespace CookiesAuth.Reveal
{
    internal class SampleUserContextProvider : RVBaseUserContextProvider
    {
        protected override RVUserContext GetUserContext(HttpContext aspnetContext)
        {
            //we don't have a real authentication in this sample, so we're just hard-coding "guest" as the user here
            //when using standard auth mechanisms, the userId can be obtained using something like aspnetContext.User.Identity.Name.
            string userId = "guest";

            //RVUserContext allows to store properties in addition to the userId, these properties can be used later
            //for example in the authentication provider. You could store data related to the current request this way. In this case, we're just storing a sample header data.
            return new RVUserContext(
                userId,
                new Dictionary<string, object>() { { "samplesessionheader", "sampleSessionHeaderValue" } });
        }
    }
}
