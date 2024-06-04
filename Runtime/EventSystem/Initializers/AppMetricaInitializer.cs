using Io.AppMetrica;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;
using UnityEngine;

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

                AppMetrica.Activate(new AppMetricaConfig(apiKey) {
                    FirstActivationAsUpdate = !IsFirstLaunch(),
                });
            }
        }
        
        private static bool IsFirstLaunch()
        {
            if (PlayerPrefs.GetInt("IsFirstLaunch", 0) == 0)
            {
                PlayerPrefs.SetInt("IsFirstLaunch", 1);
                return true;
            }
            
            return false;
        }
    }
}
