using Reveal.Sdk.Samples.Web.UpMedia.Backend.Services;
using System;
using System.Threading.Tasks;

namespace Reveal.Sdk.Samples.Web.UpMedia.Backend.SDK
{
    public class DashboardProvider: IRVDashboardProvider
    {
        readonly IDashboardService _dashboardService;

        public DashboardProvider(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public Task<Dashboard> GetDashboardAsync(IRVUserContext userContext, string dashboardId)
        {
            var dashboards = _dashboardService.GetAvailableDashboards();

            Dashboard result = dashboards.ContainsKey(dashboardId) ? dashboards[dashboardId] : null;
            return Task.FromResult(result);
        }

        public Task SaveDashboardAsync(IRVUserContext userContext, string dashboardId, Dashboard dashboard)
        {
            throw new NotImplementedException();
        }
    }
}