using Reveal.Sdk;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace CookiesAuth.Reveal
{
    public class SampleRevealSdkContext : RevealSdkContextBase
    {
        public override IRVAuthenticationProvider AuthenticationProvider { get; } = new SampleAuthenticationProvider();

        public override Task<Dashboard> GetDashboardAsync(string dashboardId)
        {
            var resourceName = $"CookiesAuth.Dashboards.{dashboardId}.rdash";

            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            var dashboard = new Dashboard(stream);

            return Task.FromResult(dashboard);
        }

        public override Task SaveDashboardAsync(string userId, string dashboardId, Dashboard dashboard)
        {
            //return complated task if save occurs
            return Task.CompletedTask;
        }
    }
}
