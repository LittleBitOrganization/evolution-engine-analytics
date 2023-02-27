using System.Collections.Generic;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;
using LittleBitGames.Environment.Events;
using Wazzitude;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    
    
    public class WazzitudeSystemEvent:IAdImpressionEvent<IDataEventAdImpression>, 
        IEcommerceEvent<IDataEventEcommerce>,
        IDesignEvent<IDataEventDesign>,
        IDesignEventWithParameters
    {
        private readonly IWazzitudeAnalytics _wazzitudeAnalytics;

        public WazzitudeSystemEvent(IWazzitudeAnalytics wazzitudeAnalytics)
        {
            _wazzitudeAnalytics = wazzitudeAnalytics;
        }
        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            _wazzitudeAnalytics.TrackRevenue(data.AdSource,data.Value,data.Currency,data.AdFormat);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            _wazzitudeAnalytics.TrackPurchase(data.ItemId, data.Currency, data.Amount, data.Receipt,
                data.Signature);
        }

        public void DesignEvent(DataEventDesign label)
        {
            _wazzitudeAnalytics.SendEvent(label.Label);
        }

        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams)
        {
            var properties = new Dictionary<string, object>();
            
            foreach (var param in designWithParams.EventParameters)
            {
                properties.Add(param.Name,param.ValueDouble);
            }
            _wazzitudeAnalytics.SendEvent(designWithParams.Label,properties);
        }
        
        
    }
}