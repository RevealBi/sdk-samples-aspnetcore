using Reveal.Sdk.Samples.Web.UpMedia.Backend.Services;
using System;
using System.Threading.Tasks;

namespace Reveal.Sdk.Samples.Web.UpMedia.Backend.SDK
{
    public class RevealSdkContext : RevealSdkContextBase
    {
        readonly IDashboardService _dashboardService;

        public RevealSdkContext(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public override Task<Dashboard> GetDashboardAsync(string dashboardId)
        {
            var dashboards = _dashboardService.GetAvailableDashboards();

            Dashboard result = dashboards.ContainsKey(dashboardId) ? dashboards[dashboardId] : null;
            return Task.FromResult(result);
        }

        public override Task SaveDashboardAsync(string userId, string dashboardId, Dashboard dashboard)
        {
            throw new NotImplementedException();
        }
    }
}
