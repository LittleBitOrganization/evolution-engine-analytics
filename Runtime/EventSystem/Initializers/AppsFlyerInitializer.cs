using AppsFlyerSDK;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AppsFlyerInitializer:IInitializer
    {
        public void Start()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.AppsFlyer))
            {
                var devKey = analyticsConfig.AppsFlyerDevKey;
                var appID = "";
#if UNITY_IOS
                appID = analyticsConfig.AppsFlyerAppID;
#endif
                AppsFlyer.initSDK(devKey, appID);
                AppsFlyer.startSDK();
            }
        }
    }
}