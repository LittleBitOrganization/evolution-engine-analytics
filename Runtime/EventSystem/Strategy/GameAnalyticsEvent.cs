using System.Collections.Generic;
using Events;
using GameAnalyticsSDK;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class GameEvent : 
        ICurrencyEvent<IDataEventCurrency>, 
        IDesignEvent<IDataEventDesign>,
        IEcommerceEvent<IDataEventEcommerce>,
        IAdImpressionEvent<IDataEventAdImpression>
    {
        public void EarnVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source,
                dataEventCurrency.ResourceId,
                (float) dataEventCurrency.CountResources,
                dataEventCurrency.Type,
                dataEventCurrency.PlaceId);
           
        }

        public void SpendVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink,
                dataEventCurrency.ResourceId,
                (float) dataEventCurrency.CountResources,
                dataEventCurrency.Type,
                dataEventCurrency.PlaceId);
        }
        
        public void DesignEvent(DataEventDesign dataEventDesign)
        {
            GameAnalytics.NewDesignEvent(dataEventDesign.Label);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            GameAnalytics.NewBusinessEvent(data.Currency,(int) (data.Amount * 100), data.ItemType, data.ItemId, data.CartType);
        }

        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Currency", data.Currency);
            fields.Add("Value", data.Value);
            GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.Undefined,data.AdSource, data.AdUnitName,
                            fields);
        }
    }
}