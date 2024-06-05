using System.Collections.Generic;
using Io.AppMetrica;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters;
using LittleBitGames.Environment;
using LittleBitGames.Environment.Events;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR
using Io.AppMetrica.Native.Android;
#elif (UNITY_IPHONE || UNITY_IOS) && !UNITY_EDITOR
using Io.AppMetrica.Native.Ios;
#else
using Io.AppMetrica.Native.Dummy;
#endif

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AppMetricaEvent : ICurrencyEvent<IDataEventCurrency>,
        IDesignEvent<IDataEventDesign>,
        IAdImpressionEvent<IDataEventAdImpression>,
        IDesignEventWithParameters, 
        IEcommerceEvent<IDataEventEcommerce>
    {
        private readonly ExecutionMode _executionMode;
        
        public AppMetricaEvent(ExecutionMode executionMode)
        {
            _executionMode = executionMode;
        }
        
        public void EarnVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            AppMetrica.ReportEvent(CustomEventNames.EarnVirtualCurrency, JsonConvert.SerializeObject(new Dictionary<string, string>()
            {
                { "ResourceId", dataEventCurrency.ResourceId },
                { "Value", dataEventCurrency.CountResources.ToString() },
                { "ItemType", dataEventCurrency.Type },
                { "PlaceId", dataEventCurrency.PlaceId }
            }));
        }

        public void SpendVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            AppMetrica.ReportEvent(CustomEventNames.SpendVirtualCurrency, JsonConvert.SerializeObject(new Dictionary<string, string>()
            {
                { "ResourceId", dataEventCurrency.ResourceId },
                { "Value", dataEventCurrency.CountResources.ToString() },
                { "ItemType", dataEventCurrency.Type },
                { "PlaceId", dataEventCurrency.PlaceId }
            }));
        }

        public void DesignEvent(DataEventDesign label)
        {
            AppMetrica.ReportEvent(label.Label);
        }

        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            AppMetrica.ReportAdRevenue(new AdRevenue(data.Value, data.Currency));
        }
        
        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            
            for (int i = 0; i < designWithParams.EventParameters.Length; i++)
            {
                var parameter = designWithParams.EventParameters[i];
                parameters.Add(parameter.Name, parameter.ConvertValueToString());
            }
            
            AppMetrica.ReportEvent(designWithParams.Label, JsonConvert.SerializeObject(parameters));
        }
        
        public void EcommercePurchase(IDataEventEcommerce data)
        {
            if (_executionMode == ExecutionMode.Production)
            {
                Revenue revenue = new Revenue((long)data.Amount, data.Currency);
                revenue.ProductID = data.ItemId;
                
                AppMetrica.ReportRevenue(revenue);
            }
        }
    }
}