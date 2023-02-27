using System;
using System.Collections.Generic;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;
using LittleBitGames.Environment.Events;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Strategy
{
    public class AmplitudeEvent: IAdImpressionEvent<IDataEventAdImpression>, 
        IEcommerceEvent<IDataEventEcommerce>,
        IDesignEvent<IDataEventDesign>,
        IDesignEventWithParameters
    {
        private readonly string amFirstOpenPrefs = "f34f43gh565hg2g234g43h_open_sent";
        
        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            var revenue = data.Value;
            if (revenue < 0) return;

            var impressionEventDataParameters = new Dictionary<string, object>
                    {{"$revenue", revenue}};

            Debug.Log($"Track amplitude revenue ${revenue}");
            Amplitude.Instance.logRevenue(data.AdFormat, 1, revenue, "", "",
                "ads", impressionEventDataParameters);
        }

        public void EcommercePurchase(IDataEventEcommerce data)
        {
            Amplitude.Instance.setUserProperty("playing_days", DaysOfPlayingInt);
            
            var impressionEventDataParameters = new Dictionary<string, object>
                {{"$currency", data.Currency}};
            Amplitude.Instance.logRevenue(data.ItemId, 1,
                data.Amount,
                data.Receipt,
                data.Signature,
                "inapp", impressionEventDataParameters);
        }

        public void DesignEvent(DataEventDesign label)
        {
            Amplitude.Instance.setUserProperty("playing_days", DaysOfPlayingInt);
            Amplitude.Instance.logEvent(label.Label);
        }
        public void DesignEventWithParameters(DataEventDesignWithParams designWithParams)
        {
            var properties = new Dictionary<string, object>();
            
            foreach (var param in designWithParams.EventParameters)
            {
                properties.Add(param.Name,param.ValueDouble);
            }
            Amplitude.Instance.setUserProperty("playing_days", DaysOfPlayingInt);
            Amplitude.Instance.logEvent(designWithParams.Label, properties);
        }
        
        private int daysOfPlayingInt = -1;

        private int DaysOfPlayingInt
        {
            get
            {
                if (daysOfPlayingInt >= 0)
                {
                    return daysOfPlayingInt;
                }

                int firstOpenTime = 0;
                string firstOpenTimeString = PlayerPrefs.GetString(amFirstOpenPrefs, null);
                DateTime epochStart = new DateTime(2010, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
                if (firstOpenTimeString != null && int.TryParse(firstOpenTimeString, out firstOpenTime) &&
                    firstOpenTime > 0)
                {
                    int delta = (int) (DateTime.UtcNow - epochStart).TotalSeconds - firstOpenTime;
                    daysOfPlayingInt = Mathf.FloorToInt(delta / 86400f);
                }
                else
                {
                    PlayerPrefs.SetString(amFirstOpenPrefs,
                        ((int) (DateTime.UtcNow - epochStart).TotalSeconds).ToString());
                    daysOfPlayingInt = 0;
                }

                return daysOfPlayingInt;
            }
        }

        
    }
}