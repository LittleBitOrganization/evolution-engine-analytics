using System;
using com.adjust.sdk;
using LittleBitGames.Environment.Events;
using NaughtyAttributes;
using RemoteConfig;
using UnityEngine;
namespace LittleBit.Modules.Analytics.EventSystem.Configs
{
    [Serializable]
    public class EventMask
    {
        [SerializeField, EnumFlags] private EventsServiceType _adRevenuePaidEventServices = EventsServiceType.Everything;
        [SerializeField, EnumFlags] private EventsServiceType _IAPEventServices = EventsServiceType.Everything;
        
        [SerializeField, EnumFlags] private EventsServiceType _designEventServices = EventsServiceType.Everything;
        [SerializeField, EnumFlags] private EventsServiceType _designWithParamsEventServices = EventsServiceType.Everything;
        
        public EventsServiceType AdRevenuePaidEventServices => _adRevenuePaidEventServices;
        public EventsServiceType IAPEventServices => _IAPEventServices;
        public EventsServiceType DesignWithParamsEventServices => _designWithParamsEventServices;
        public EventsServiceType DesignEventServices => _designEventServices;
        public EventsServiceType EnabledServices => AdRevenuePaidEventServices | IAPEventServices | DesignWithParamsEventServices | DesignEventServices;
    }
    public class AnalyticsConfig : ScriptableObject
    {
        [InfoBox("Устаревшее. Включенные сервисы вычисляются путем сложения масок ивентов", EInfoBoxType.Warning)]
        [SerializeField, EnumFlags, Obsolete] private EventsServiceType enabledServices = EventsServiceType.Everything;

        [SerializeField] private EventMask _eventMask;
       
        
        [SerializeField, ShowIf(nameof(IsEnableAdjust))] private AdjustSettings _adjustSettings;

        [SerializeField, ShowIf(nameof(IsEnableAmplitude))] private string _amplitude_api_key;
        
        [SerializeField, ShowIf(nameof(IsEnableWazzitude))] private string _wazzitude_url;
        
        [SerializeField, ShowIf(nameof(IsEnableAppsFlyer))] private string _appsflyer_dev_key;
#if UNITY_IOS        
        [SerializeField, ShowIf(nameof(IsEnableAppsFlyer))] private string _appsflyer_ios_app_ID;
        public string AppsFlyerAppID => _appsflyer_ios_app_ID;
#endif

        public EventsServiceType EnabledServices => _eventMask.EnabledServices;

        public EventMask EventMask => _eventMask;
       
        

        public string WazzitudeUrl => _wazzitude_url;
        public string AmplitudeApiKey => _amplitude_api_key;
        public string AppsFlyerDevKey => _appsflyer_dev_key;
        public AdjustSettings AdjustSettings => _adjustSettings;
        public AdjustConfig AdjustConfig
        {
            get
            {
                if (IsEnableAdjust)
                    return _adjustSettings.Create();
                else
                    return null;
            }
        }

        [field: SerializeField] public int RemoteConfigCacheExpiration { get; private set; }
        [field: SerializeField] public FallbackConfig FallbackRemoteConfig { get; private set; }

        internal bool IsEnableService(EventsServiceType type) => EnabledServices.IsEnableService(type);
        
        private bool IsEnableWazzitude => IsEnableService(EventsServiceType.Wazzitude);
        private bool IsEnableAdjust => IsEnableService(EventsServiceType.Adjust);
        private bool IsEnableAmplitude => IsEnableService(EventsServiceType.Amplitude);
        private bool IsEnableGa => IsEnableService(EventsServiceType.GA);
        private bool IsEnableFireBase => IsEnableService(EventsServiceType.Firebase);
        private bool IsEnableAppsFlyer => IsEnableService(EventsServiceType.AppsFlyer);
    }

    [Serializable]
    public class AdjustSettings
    {
        [SerializeField] private string _adjustAppToken;
        [SerializeField] private AdjustEnvironment _adjustEnvironment;
        [SerializeField] private AdjustAppSecret _adjustAppSecret;
        [SerializeField] private bool _eventBuffering = false;
        [SerializeField] private bool _sendInBackground = false;
        [SerializeField] private string _purchaseEventToken;
        [SerializeField] private AdjustLogLevel _logLevel;
        public string AdjustAppToken => _adjustAppToken;
        public AdjustEnvironment AdjustEnvironment => _adjustEnvironment;
        public AdjustAppSecret AdjustAppSecret => _adjustAppSecret;
        public bool EventBuffering => _eventBuffering;
        public bool SendInBackground => _sendInBackground;
        public string PurchaseEventToken => _purchaseEventToken;
        public AdjustLogLevel LogLevel => _logLevel;

        [field: SerializeField] public TokenRecord[] Tokens { get; private set; }

        public AdjustConfig Create()
        {
            var adjustConfig = new AdjustConfig(AdjustAppToken, AdjustEnvironment);
            adjustConfig.setSendInBackground(SendInBackground);
            adjustConfig.setEventBufferingEnabled(EventBuffering);
            adjustConfig.setLogLevel(LogLevel);
            adjustConfig.setAppSecret(
                AdjustAppSecret.SecretId,
                AdjustAppSecret.Info1,
                AdjustAppSecret.Info2,
                AdjustAppSecret.Info3,
                AdjustAppSecret.Info4);
            return adjustConfig;
        }

        [Serializable]
        public struct TokenRecord
        {
            [field: SerializeField] public string EventLabel { get; private set; }
            [field: SerializeField] public string Token { get; private set; }
        }
        
    }

    [Serializable]
    public class AdjustAppSecret
    {
        [SerializeField] private long _secretId = 0;
        [SerializeField] private long _info1 = 0;
        [SerializeField] private long _info2 = 0;
        [SerializeField] private long _info3 = 0;
        [SerializeField] private long _info4 = 0;
        
        public long SecretId => _secretId;
        public long Info1 => _info1;
        public long Info2 => _info2;
        public long Info3 => _info3;
        public long Info4 => _info4;
    }

    public static class EventsServiceTypeExtension
    {
        public static bool IsEnableService(this EventsServiceType enabledServices, EventsServiceType targetType)
        {
            return (enabledServices & targetType) == targetType;
        }
    }
}
