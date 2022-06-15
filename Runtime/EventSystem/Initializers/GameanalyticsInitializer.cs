using GameAnalyticsSDK;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Services;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class GameanalyticsInitializer : MonoBehaviour
    {
        private void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.GA))
            {
                GameAnalytics.Initialize();
                GameAnalyticsILRD.SubscribeMaxImpressions();
            }
        }
    }
}