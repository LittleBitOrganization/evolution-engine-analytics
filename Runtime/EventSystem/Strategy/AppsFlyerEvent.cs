using System.Collections.Generic;
using AppsFlyerSDK;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters;
using LittleBitGames.Environment.Events;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AppsFlyerEvent : IAdImpressionEvent<IDataEventAdImpression>,
        IEcommerceEvent<IDataEventEcommerce>,
        IDesignEventWithParameters,
        IDesignEvent<IDataEventDesign>
    {
        private readonly ExecutionMode _executionMode;

        public AppsFlyerEvent(ExecutionMode executionMode)
        {
            _executionMode = executionMode;
        }
        
        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            // Debug.LogError("AppsFlyer value =" + data.Value);
            // Debug.LogError("AppsFlyer Currency =" + data.Currency);
            // Debug.LogError("AppsFlyer value.ToString =" + data.Value.ToString());
            // Debug.LogError("AppsFlyer value.ToString(US) =" +
            //                data.Value.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            // Dictionary<string, string> eventValues = new Dictionary<string, string>();
            // eventValues.Add("af_adSource", data.AdSource);
            // eventValues.Add(AFInAppEvents.REVENUE, data.Value.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
            // eventValues.Add(AFInAppEvents.CURRENCY, data.Currency);
            // eventValues.Add("af_adFormat", data.AdFormat);
            // AppsFlyer.sendEvent("af_adImpression", eventValues);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            if (_executionMode == ExecutionMode.Production)
            {
                Dictionary<string, string> eventValues = new Dictionary<string, string>();
                eventValues.Add(AFInAppEvents.CURRENCY, data.Currency);
                eventValues.Add(AFInAppEvents.REVENUE, data.Amount.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
                eventValues.Add("af_quantity", "1");
                AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
            }
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
