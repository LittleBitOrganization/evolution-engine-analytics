using System.Collections.Generic;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBitGames.Environment.Events;

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
            AppMetrica.Instance.ReportEvent(CustomEventNames.EarnVirtualCurrency,
                new Dictionary<string, object>()
                {
                    { "ResourceId", dataEventCurrency.ResourceId },
                    { "Value", dataEventCurrency.CountResources },
                    { "ItemType", dataEventCurrency.Type },
                    { "PlaceId", dataEventCurrency.PlaceId }
                });
        }

        public void SpendVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            AppMetrica.Instance.ReportEvent(CustomEventNames.SpendVirtualCurrency,
                new Dictionary<string, object>()
                {
                    { "ResourceId", dataEventCurrency.ResourceId },
                    { "Value", dataEventCurrency.CountResources },
                    { "ItemType", dataEventCurrency.Type },
                    { "PlaceId", dataEventCurrency.PlaceId }
                });
        }

        public void DesignEvent(DataEventDesign label)
        {
            AppMetrica.Instance.ReportEvent(label.Label);
        }

        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            YandexAppMetricaAdRevenue adRevenue = new YandexAppMetricaAdRevenue(data.Value, data.Currency);
            AppMetrica.Instance.ReportAdRevenue(adRevenue);
        }
        
        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            for (int i = 0; i < designWithParams.EventParameters.Length; i++)
            {
                var parameter = designWithParams.EventParameters[i];
                parameters.Add(parameter.Name, parameter.ValueString);
            }
            AppMetrica.Instance.ReportEvent(designWithParams.Label, parameters);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            if (_executionMode == ExecutionMode.Production)
            {
                YandexAppMetricaRevenue revenue = new YandexAppMetricaRevenue((decimal)data.Amount, data.Currency);
                AppMetrica.Instance.ReportRevenue(revenue);
            }
        }
    }
}