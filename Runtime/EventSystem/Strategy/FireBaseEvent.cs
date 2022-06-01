using Firebase.Analytics;
using LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public static class CustomEventNames
    {
        public const string EarnVirtualCurrency = "EarnVirtualCurrency";
        public const string SpendVirtualCurrency = "SpendVirtualCurrency";
    }

    public class FireBaseEvent :
        ICurrencyEvent<IDataEventCurrency>,
        IDesignEvent<IDataEventDesign>,
        IAdImpressionEvent<IDataEventAdImpression>,
        IDesignEventWithParameters
    {
        public void EarnVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            FirebaseAnalytics.LogEvent(CustomEventNames.EarnVirtualCurrency,
                new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, dataEventCurrency.ResourceId),
                new Parameter(FirebaseAnalytics.ParameterValue, dataEventCurrency.CountResources),
                new Parameter("ItemType", dataEventCurrency.Type),
                new Parameter("PlaceId", dataEventCurrency.PlaceId));
            
        }

        public void SpendVirtualCurrency(IDataEventCurrency dataEventCurrency)
        {
            FirebaseAnalytics.LogEvent(CustomEventNames.SpendVirtualCurrency,
                new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, dataEventCurrency.ResourceId),
                new Parameter(FirebaseAnalytics.ParameterValue, dataEventCurrency.CountResources),
                new Parameter("ItemType", dataEventCurrency.Type),
                new Parameter("PlaceId", dataEventCurrency.PlaceId));
        }

        public void DesignEvent(DataEventDesign dataEventDesign)
        {
            FirebaseAnalytics.LogEvent(dataEventDesign.Label);
        }

        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams)
        {
            //string message = "FirebaseEvent: " + designWithParams.Label + "\n";
            Parameter[] parameters = new Parameter[designWithParams.EventParameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                EventParameter eventParameter = designWithParams.EventParameters[i];
                parameters[i] = eventParameter.ConvertToFirebaseParameter();

                //message += "\t" + eventParameter.Name + ": " + eventParameter.ConvertValueToString() + "\n";
            }

            FirebaseAnalytics.LogEvent(designWithParams.Label, parameters);
            //Debug.LogError(message);
        }

        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventAdImpression,
                new Parameter(FirebaseAnalytics.ParameterAdSource, data.AdSource),
                new Parameter(FirebaseAnalytics.ParameterAdFormat, data.AdFormat),
                new Parameter(FirebaseAnalytics.ParameterAdUnitName, data.AdUnitName),
                new Parameter(FirebaseAnalytics.ParameterCurrency, data.Currency),
                new Parameter(FirebaseAnalytics.ParameterValue, data.Value));
        }
    }
}