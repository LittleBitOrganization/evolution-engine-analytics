using GameAnalyticsSDK;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class GameanalyticsInitializer : IInitializer
    {
        public void Start()
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