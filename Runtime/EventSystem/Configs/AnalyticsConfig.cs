using System;
using com.adjust.sdk;
using NaughtyAttributes;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Configs
{
    public class AnalyticsConfig : ScriptableObject
    {
        
        [SerializeField, EnumFlags] private EventsServiceType enabledServices = EventsServiceType.Everything;

        [SerializeField, ShowIf(nameof(IsEnableAdjust))] private AdjustSettings _adjustSettings;
        
        
        public EventsServiceType EnabledServices => enabledServices;


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
        
        
        internal bool IsEnableService(EventsServiceType type) => enabledServices.IsEnableService(type);
        
        private bool IsEnableAdjust => IsEnableService(EventsServiceType.Adjust);
        private bool IsEnableGa => IsEnableService(EventsServiceType.GA);
        private bool IsEnableFireBase => IsEnableService(EventsServiceType.Firebase);
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