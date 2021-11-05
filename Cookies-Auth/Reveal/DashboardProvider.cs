using Reveal.Sdk;
using System.Reflection;
using System.Threading.Tasks;

namespace CookiesAuth.Reveal
{
    public class DashboardProvider :IRVDashboardProvider
    {
        public Task<Dashboard> GetDashboardAsync(IRVUserContext userContext, string dashboardId)
        {
            var resourceName = $"CookiesAuth.Dashboards.{dashboardId}.rdash";

            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            var dashboard = new Dashboard(stream);

            return Task.FromResult(dashboard);
        }

        public Task SaveDashboardAsync(IRVUserContext userContext, string dashboardId, Dashboard dashboard)
        {
            //return complated task if save occurs
            return Task.CompletedTask;
        }
    }
}