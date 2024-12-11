using System;
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
        private readonly Action<bool> _onPurchaseConnectorInit;

        public AppsFlyerInitializer(Action<bool> onPurchaseConnectorInit)
        {
            _onPurchaseConnectorInit = onPurchaseConnectorInit;
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
                
                _onPurchaseConnectorInit?.Invoke(analyticsConfig.Mode == ExecutionMode.Debug);
                
                AppsFlyer.startSDK();
            }
        }
    }
}