using AppsFlyerConnector;
using AppsFlyerSDK;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment;
using LittleBitGames.Environment.Events;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AppsFlyerInitializer:IInitializer
    {
        private readonly AnalyticsInitializer _analyticsInitializer;

        public AppsFlyerInitializer(AnalyticsInitializer analyticsInitializer)
        {
            _analyticsInitializer = analyticsInitializer;
        }
        
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
                AppsFlyer.enableTCFDataCollection(true);
                AppsFlyer.initSDK(devKey, appID);
                
                AppsFlyerPurchaseConnector.init(_analyticsInitializer, AppsFlyerConnector.Store.GOOGLE);
                AppsFlyerPurchaseConnector.setIsSandbox(analyticsConfig.Mode == ExecutionMode.Debug);
                AppsFlyerPurchaseConnector.setAutoLogPurchaseRevenue(
                    AppsFlyerAutoLogPurchaseRevenueOptions.AppsFlyerAutoLogPurchaseRevenueOptionsAutoRenewableSubscriptions |
                    AppsFlyerAutoLogPurchaseRevenueOptions.AppsFlyerAutoLogPurchaseRevenueOptionsInAppPurchases
                );
                AppsFlyerPurchaseConnector.build();
                AppsFlyerPurchaseConnector.startObservingTransactions();
                
                AppsFlyer.startSDK();
            }
        }
    }
}