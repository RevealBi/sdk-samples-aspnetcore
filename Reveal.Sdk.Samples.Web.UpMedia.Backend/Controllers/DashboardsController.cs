using Microsoft.AspNetCore.Mvc;
using Reveal.Sdk.Samples.Web.UpMedia.Backend.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Reveal.Sdk.Samples.Web.UpMedia.SDK.Backend.Controllers
{
    [Route("Dashboards")]
    public class DashboardsController : Controller
    {
        readonly IDashboardService _dashboardService;
        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<DashboardInfo[]> GetDashboardInfos()
        {
            var tasks = _dashboardService.GetAvailableDashboards().Select(async kvp => await kvp.Value.GetInfoAsync(kvp.Key));

            return await Task.WhenAll(tasks);
        }
    }
}
