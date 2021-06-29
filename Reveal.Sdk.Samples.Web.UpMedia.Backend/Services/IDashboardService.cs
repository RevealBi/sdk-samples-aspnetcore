using System.Collections.ObjectModel;

namespace Reveal.Sdk.Samples.Web.UpMedia.Backend.Services
{
    public interface IDashboardService
    {
        public ReadOnlyDictionary<string, Dashboard> GetAvailableDashboards();
        public Dashboard GetDashboardByName(string name);

        public void UpdateDashboardByName(string name, Dashboard dashboard);
        public void AddNewDashboard(string name, Dashboard dashboard);
    }
}