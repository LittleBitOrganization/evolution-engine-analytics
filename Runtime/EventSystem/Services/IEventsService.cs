using Events;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;

namespace LittleBit.Modules.Analytics.EventSystem.Services
{
    public interface IEventsService
    {
        void SpendVirtualCurrency(DataEventCurrency dataEventCurrency,
            EventsServiceType flags = EventsServiceType.Everything);

        void EarnVirtualCurrency(DataEventCurrency dataEventCurrency,
            EventsServiceType flags = EventsServiceType.Everything);

        void DesignEvent(DataEventDesign dataEventDesign,
            EventsServiceType flags = EventsServiceType.Everything);

        void DesignEventWithParams(DataEventDesignWithParams dataEventDesignWithParams,
            EventsServiceType flags = EventsServiceType.Everything);

        void AdRevenuePaidEvent(IDataEventAdImpression data,
            EventsServiceType flags = EventsServiceType.Everything);

        void EcommercePurchase(IDataEventEcommerce dataEventEcommerce);
    }
}