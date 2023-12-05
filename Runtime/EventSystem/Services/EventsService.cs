using System;
using System.Collections.Generic;
using System.Linq;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Strategy;
using LittleBitGames.Environment.Ads;
using LittleBitGames.Environment.Events;
using LittleBitGames.Environment.Purchase;
#if WAZZITUDE
using Wazzitude;
#endif

namespace LittleBit.Modules.Analytics.EventSystem.Services
{
    public class EventsService : IEventsService, IAdsEventService,IIAPEventService
    {
        private readonly List<ICurrencyEvent<IDataEventCurrency>> _analyticsCurrencies;
        private readonly List<IAdImpressionEvent<IDataEventAdImpression>> _analyticsAdImpression;
        private readonly List<IDesignEvent<IDataEventDesign>> _designEvents;
        private readonly List<IDesignEventWithParameters> _designEventsWithParameters;
        private readonly List<IEcommerceEvent<IDataEventEcommerce>> _ecommerceEvents;

        private AnalyticsConfig _config;

        public EventsService()
        {
            _config = new AnalyticsConfigFactory().Create();

            _analyticsAdImpression = new List<IAdImpressionEvent<IDataEventAdImpression>>()
            {
                new FireBaseEvent(),
                new GameEvent(),
                new AmplitudeEvent(),
#if WAZZITUDE
                new WazzitudeSystemEvent(WazzitudeAnalytics.Instance),
#endif
                new AppsFlyerEvent()
            };

            _analyticsCurrencies = new List<ICurrencyEvent<IDataEventCurrency>>()
            {
                new GameEvent(),
                new FireBaseEvent(),
            };
            
            _designEvents = new List<IDesignEvent<IDataEventDesign>>()
            {
                new GameEvent(),
                new FireBaseEvent(),
                new AmplitudeEvent(),
#if WAZZITUDE
                new WazzitudeSystemEvent(WazzitudeAnalytics.Instance),
#endif
                new AppsFlyerEvent()
            };

            _designEventsWithParameters = new List<IDesignEventWithParameters>()
            {
                new FireBaseEvent(),
                new AmplitudeEvent(),
#if WAZZITUDE
                new WazzitudeSystemEvent(WazzitudeAnalytics.Instance),
#endif
                new AppsFlyerEvent()
            };

            _ecommerceEvents = new List<IEcommerceEvent<IDataEventEcommerce>>()
            {
                new GameEvent(),
                new AmplitudeEvent(),
#if WAZZITUDE
                new WazzitudeSystemEvent(WazzitudeAnalytics.Instance),
#endif
                new AppsFlyerEvent()
            };
        }

        //TODO отсутсвует маска в конфиге для фильтрации
        public void SpendVirtualCurrency(DataEventCurrency dataEventCurrency,
            EventsServiceType flags = EventsServiceType.Everything)
        {
            foreach (var analyticsCurrency in FilterEventSystems(_analyticsCurrencies, flags))
            {
                analyticsCurrency.SpendVirtualCurrency(new DataEventCurrency(dataEventCurrency.ResourceId,
                    dataEventCurrency.CountResources,
                    dataEventCurrency.Type,
                    dataEventCurrency.PlaceId));
            }
        }

        //TODO отсутсвует маска в конфиге для фильтрации
        public void EarnVirtualCurrency(DataEventCurrency dataEventCurrency,
            EventsServiceType flags = EventsServiceType.Everything)
        {
            foreach (var analyticsCurrency in FilterEventSystems(_analyticsCurrencies, flags))
            {
                analyticsCurrency.EarnVirtualCurrency(new DataEventCurrency(dataEventCurrency.ResourceId,
                    dataEventCurrency.CountResources,
                    dataEventCurrency.Type,
                    dataEventCurrency.PlaceId));
            }
        }

        public void DesignEvent(DataEventDesign dataEventDesign, EventsServiceType flags = EventsServiceType.Everything)
        {
            flags &= _config.EventMask.DesignEventServices;
            foreach (var eventSystem in FilterEventSystems(_designEvents, flags))
            {
                eventSystem.DesignEvent(dataEventDesign);
            }
        }

        private void DesignEventResidual(DataEventDesign dataEventDesign, List<Type> excludeTypesEventServices,
            EventsServiceType flags)
        {
            foreach (var eventSystem in FilterEventSystems(_designEvents, flags))
            {
                if (excludeTypesEventServices.Contains(eventSystem.GetType()))
                    continue;

                eventSystem.DesignEvent(dataEventDesign);
            }
        }

        public void DesignEventWithParams(DataEventDesignWithParams dataEventDesignWithParams,
            EventsServiceType flags = EventsServiceType.Everything)
        {
            flags &= _config.EventMask.DesignWithParamsEventServices;
            var listExcludeEventServices = new List<Type>();

            foreach (var eventSystem in FilterEventSystems(_designEventsWithParameters, flags))
            {
                eventSystem.DesignEventWithParameters(dataEventDesignWithParams);

                listExcludeEventServices.Add(eventSystem.GetType());
            }

            DesignEventResidual(dataEventDesignWithParams, listExcludeEventServices, flags);
        }

        private List<T> FilterEventSystems<T>(List<T> systems, EventsServiceType flags = EventsServiceType.Everything)
        {
            var mask = _config.EnabledServices & flags;

            var clone = systems.ToList();

            if (mask.HasFlag(EventsServiceType.Firebase) == false) clone.RemoveAll(s => s is FireBaseEvent);
            if (mask.HasFlag(EventsServiceType.GA) == false) clone.RemoveAll(s => s is GameEvent);
            if (mask.HasFlag(EventsServiceType.Amplitude) == false) clone.RemoveAll(s => s is AmplitudeEvent);
#if WAZZITUDE
            if (mask.HasFlag(EventsServiceType.Wazzitude) == false) clone.RemoveAll(s => s is WazzitudeSystemEvent);
#endif
            if (mask.HasFlag(EventsServiceType.AppsFlyer) == false) clone.RemoveAll(s => s is AppsFlyerEvent);

            return clone;
        }

        public void AdRevenuePaidEvent(IDataEventAdImpression data, EventsServiceType flags = EventsServiceType.Everything)
        {
            flags &= _config.EventMask.AdRevenuePaidEventServices;
            foreach (var analytics in FilterEventSystems(_analyticsAdImpression, flags))
            {
                analytics.AdRevenuePaidEvent(data);
            }
        }
        
        public void EcommercePurchase(IDataEventEcommerce dataEventEcommerce)
        {
            EventsServiceType flags = _config.EventMask.IAPEventServices;
            foreach (var e in FilterEventSystems(_ecommerceEvents, flags))
            {
                e.EcommercePurchase(dataEventEcommerce);
            }
        }
    }
}