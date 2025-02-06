using System;
using System.Collections.Generic;
using LittleBitGames.Environment;
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
        [SerializeField] private ExecutionMode mode;
        [InfoBox("Устаревшее. Включенные сервисы вычисляются путем сложения масок ивентов", EInfoBoxType.Warning)]
        [SerializeField, EnumFlags, Obsolete] private EventsServiceType enabledServices = EventsServiceType.Everything;

        [SerializeField] private EventMask _eventMask;
        
        
        [field: SerializeField, ShowIf(nameof(IsEnableAppMetrica))] public string ApiKeyAppMetrica { get; private set; }
        [field: SerializeField] public List<AnalyticsServiceConfig> AdditionalServiceConfig { get; private set; }

        public EventsServiceType EnabledServices => _eventMask.EnabledServices;

        public EventMask EventMask => _eventMask;
        
        public ExecutionMode Mode => mode;
        
        [field: SerializeField] public int RemoteConfigCacheExpiration { get; private set; }
        [field: SerializeField] public FallbackConfig FallbackRemoteConfig { get; private set; }

        internal bool IsEnableService(EventsServiceType type) => EnabledServices.IsEnableService(type);
        
        private bool IsEnableAdjust => IsEnableService(EventsServiceType.Adjust);
        private bool IsEnableGa => IsEnableService(EventsServiceType.GA);
        private bool IsEnableFireBase => IsEnableService(EventsServiceType.Firebase);

        private bool IsEnableAppMetrica => IsEnableService(EventsServiceType.AppMetrica);
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
