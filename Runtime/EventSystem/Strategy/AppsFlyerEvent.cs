﻿using System.Collections.Generic;
using AppsFlyerSDK;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters;
using LittleBitGames.Environment.Events;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AppsFlyerEvent : IAdImpressionEvent<IDataEventAdImpression>,
        IEcommerceEvent<IDataEventEcommerce>,
        IDesignEventWithParameters,
        IDesignEvent<IDataEventDesign>
    {
        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            Dictionary<string, string> eventValues = new Dictionary<string, string>();
            eventValues.Add("af_adSource", data.AdSource);
            eventValues.Add(AFInAppEvents.REVENUE, data.Value.ToString());
            eventValues.Add(AFInAppEvents.CURRENCY, data.Currency);
            eventValues.Add("af_adFormat", data.AdFormat);
            AppsFlyer.sendEvent("af_adImpression", eventValues);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            Dictionary<string, string> eventValues = new Dictionary<string, string>();
            eventValues.Add(AFInAppEvents.CURRENCY, data.Currency);
            eventValues.Add(AFInAppEvents.REVENUE, data.Amount.ToString());
            eventValues.Add("af_quantity", "1");
            AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
        }

        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams)
        {
            var properties = new Dictionary<string, string>();
            foreach (var param in designWithParams.EventParameters)
            {
                properties.Add(param.Name, param.ConvertValueToString());
            }
            AppsFlyer.sendEvent(designWithParams.Label, properties);
        }
        
        public void DesignEvent(DataEventDesign label)
        {
            var properties = new Dictionary<string, string>();
            AppsFlyer.sendEvent(label.Label, properties);
        }
    }
}