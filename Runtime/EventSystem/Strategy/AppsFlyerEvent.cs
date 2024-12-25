using System.Collections.Generic;
using System.Globalization;
using AppsFlyerConnector;
using AppsFlyerSDK;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters;
using LittleBitGames.Environment;
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
        private readonly string _monetizationPubKey;
        private IapAnalAF _iapAnalAf;

        public AppsFlyerEvent(ExecutionMode executionMode, string monetizationPubKey)
        {
            _executionMode = executionMode;
            _monetizationPubKey = monetizationPubKey;

            _iapAnalAf = new GameObject("AppsFlyerIapValidation").AddComponent<IapAnalAF>();
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
                eventValues.Add(AFInAppEvents.REVENUE, data.Amount.ToString(CultureInfo.GetCultureInfo("en-US")));
                eventValues.Add("af_quantity", "1");

                var revenue = data.Amount.ToString(CultureInfo.InvariantCulture);
                
                StoreNamePurcahseInfoSignature(data.Receipt, out string purchaseInfo, out string signature);


#if UNITY_ANDROID
                AppsFlyer.validateAndSendInAppPurchase(_monetizationPubKey, signature, purchaseInfo,
                    revenue, data.Currency, null, _iapAnalAf);
#elif UNITY_IOS
                AppsFlyer.validateAndSendInAppPurchase(data.ItemId,
                    revenue, data.Currency, data.TransactionId, null, _iapAnalAf);    
#endif
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
        
        private static void StoreNamePurcahseInfoSignature(string receipt, out string purchaseInfo, out string signature)
        {
            JSONNode jsNode = JSON.Parse(receipt);

            signature = "empty";
            purchaseInfo = "empty";

#if UNITY_IOS
            purchaseInfo = jsNode["Payload"];
#elif UNITY_ANDROID
            JSONNode payloadNode = JSON.Parse(jsNode["Payload"]);
            signature = payloadNode["signature"];
            purchaseInfo = payloadNode["json"];
#endif

            if (signature != "empty") { signature = RemoveQuotes(signature); }

            if (purchaseInfo != "empty")
            {
                purchaseInfo = RemoveQuotes(purchaseInfo);
                
            }
        }
        
        private static string RemoveQuotes(string str) {
            if (string.IsNullOrEmpty(str)) {
                Debug.Log("ERROR! RemoveQuotes: string is null or empty!");
                return "empty";
            }
            string newStr = str;

            if (str[0] == '"')
                newStr = newStr.Remove(0, 1);
            if (str[str.Length - 1] == '"')
                newStr = newStr.Remove(newStr.Length - 1, 1);

            return newStr;
        }
    }
}
