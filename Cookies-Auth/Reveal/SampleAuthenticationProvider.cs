using Reveal.Sdk;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookiesAuth.Reveal
{
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
}
