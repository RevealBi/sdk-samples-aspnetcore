using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Reveal.Sdk.Samples.Web.UpMedia.Backend.Services
{
    public class DashboardService : IDashboardService
    {
        Dictionary<string, Dashboard> _availableDashboards = null;

        public DashboardService()
        {
            var dashboardsPrefix = "Reveal.Sdk.Samples.Web.UpMedia.Backend.Dashboards.";

            var assembly = Assembly.GetExecutingAssembly();
            _availableDashboards = assembly.GetManifestResourceNames()
                .Where(name => name.Contains(dashboardsPrefix))
                .Where(name => name.EndsWith(".rdash"))
                .ToDictionary(
                    name => name.Replace(dashboardsPrefix, "").Replace(".rdash",""),
                    name => new Dashboard(assembly.GetManifestResourceStream(name)));
        }

        public ReadOnlyDictionary<string, Dashboard> GetAvailableDashboards()
        {
            return new ReadOnlyDictionary<string, Dashboard>(_availableDashboards);
        }

        public Dashboard GetDashboardByName(string name)
        {
            return _availableDashboards.ContainsKey(name) ? _availableDashboards[name] : null;
        }

        public void UpdateDashboardByName(string name, Dashboard dashboard)
        {
            _availableDashboards[name] = dashboard;
        }

        public void AddNewDashboard(string name, Dashboard dashboard)
        {
            _availableDashboards[name] = dashboard;
        }
    }
}