using GameAnalyticsSDK;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class GameEvent : 
        ICurrencyEvent<IDataEventCurrency>, 
        IDesignEvent<IDataEventDesign>
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
    }
}