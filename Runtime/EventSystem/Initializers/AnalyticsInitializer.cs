using System;
using AppsFlyerConnector;
using LittleBitGames.Environment;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AnalyticsInitializer : MonoBehaviour, IInitializer
    {
        public event Action<bool> OnFirebaseInit;
        
        public void Start()
        {
            var firebaseInitializer = new FirebaseInitializer();
            firebaseInitializer.OnFirebaseInit += b => OnFirebaseInit?.Invoke(b);
            firebaseInitializer.Start();
            (new GameanalyticsInitializer()).Start();
            (new AmplitudeInitializer()).Start();
#if WAZZITUDE
            (new WazzitudeInitializer()).Start();
#endif
            
            (new AppMetricaInitializer()).Start();
        }

        public void InitAppsflyerWithValidator(AppsflyerValidator appsflyerValidator)
        {
            (new AppsFlyerInitializer((isSandbox) =>
            {
                AppsFlyerPurchaseConnector.init(appsflyerValidator, AppsFlyerConnector.Store.GOOGLE);
                AppsFlyerPurchaseConnector.setIsSandbox(isSandbox);
                AppsFlyerPurchaseConnector.setAutoLogPurchaseRevenue(
                    AppsFlyerAutoLogPurchaseRevenueOptions.AppsFlyerAutoLogPurchaseRevenueOptionsAutoRenewableSubscriptions |
                    AppsFlyerAutoLogPurchaseRevenueOptions.AppsFlyerAutoLogPurchaseRevenueOptionsInAppPurchases
                );
                AppsFlyerPurchaseConnector.setPurchaseRevenueValidationListeners(true);
                AppsFlyerPurchaseConnector.build();
                AppsFlyerPurchaseConnector.startObservingTransactions();
                
            })).Start();
        }
    }
}