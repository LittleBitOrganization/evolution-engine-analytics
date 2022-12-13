using System.Linq;
using com.adjust.sdk;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;
using LittleBitGames.Environment.Events;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AdjustSystemEvent : IAdImpressionEvent<IDataEventAdImpression>, 
        IEcommerceEvent<IDataEventEcommerce>,
        IDesignEvent<IDataEventDesign>
    {
        private readonly AdjustSettings _adjustSettings;

        public AdjustSystemEvent(AdjustSettings adjustSettings) => _adjustSettings = adjustSettings;

        public void DesignEvent(DataEventDesign data)
        {
            var eventToken = _adjustSettings.Tokens.FirstOrDefault(t => t.EventLabel == data.Label).Token;
            
            if (!string.IsNullOrEmpty(eventToken)) Adjust.trackEvent(new AdjustEvent(eventToken));
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
            AdjustEvent adjustEvent = new AdjustEvent(_adjustSettings.PurchaseEventToken);
            
            adjustEvent.currency = data.Currency;
            adjustEvent.revenue = data.Amount;
            adjustEvent.receipt = data.Receipt;
            
            Adjust.trackEvent(adjustEvent);
        }
    }
}