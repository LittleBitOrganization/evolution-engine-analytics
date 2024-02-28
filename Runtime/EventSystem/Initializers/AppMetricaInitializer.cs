using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AppMetricaInitializer : IInitializer
    {
        public void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.AppMetrica))
            {
                var apiKey = analyticsConfig.ApiKeyAppMetrica;

                var appMetrica = AppMetrica.Init();
                appMetrica.SetupMetrica(apiKey, 300);
            }
        }
    }
}
