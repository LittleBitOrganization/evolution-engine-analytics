using com.adjust.sdk;
using Events;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AdjustSystemEvent : IAdImpressionEvent<IDataEventAdImpression>, 
        IEcommerceEvent<IDataEventEcommerce>
    {
        private string _purchaseEventToken;

        public AdjustSystemEvent(AdjustSettings adjustSettings)
        {
            _purchaseEventToken = adjustSettings.PurchaseEventToken;
        }
        
        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(data.SdkSource.Source);
            // set revenue and currency
            adjustAdRevenue.setRevenue(data.Value, data.Currency);
            // optional parameters
            adjustAdRevenue.setAdRevenueNetwork(data.AdSource);
            // track ad revenue
            Adjust.trackAdRevenue(adjustAdRevenue);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            AdjustEvent adjustEvent = new AdjustEvent(_purchaseEventToken);
            
            adjustEvent.currency = data.Currency;
            adjustEvent.revenue = data.Amount;
            adjustEvent.receipt = data.Receipt;
            
            Adjust.trackEvent(adjustEvent);
        }
    }
}