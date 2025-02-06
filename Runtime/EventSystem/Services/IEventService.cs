using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBitGames.Environment.Events;

namespace LittleBit.Modules.Analytics.EventSystem.Services
{
    public interface IEventService : ICurrencyEvent<IDataEventCurrency>,
        IDesignEvent<IDataEventDesign>,
    IAdImpressionEvent<IDataEventAdImpression>,
        IDesignEventWithParameters,
        IEcommerceEvent<IDataEventEcommerce>
    {
        
    }
}