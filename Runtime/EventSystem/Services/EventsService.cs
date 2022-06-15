using System;
using System.Collections.Generic;
using System.Linq;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using LittleBit.Modules.Analytics.EventSystem.Events.EventAdImpression;
using LittleBit.Modules.Analytics.EventSystem.Events.EventCurrency;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data;
using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Events;
using LittleBit.Modules.Analytics.EventSystem.Events.EventEncommerce;
using LittleBit.Modules.Analytics.EventSystem.Strategy;
using UnityEngine;

namespace LittleBit.Modules.Analytics.EventSystem.Services
{
    public class EventsService
    {
        private readonly List<ICurrencyEvent<IDataEventCurrency>> _analyticsCurrencies;
        private readonly List<IAdImpressionEvent<IDataEventAdImpression>> _analyticsAdImpression;
        private readonly List<IDesignEvent<IDataEventDesign>> _designEvents;
        private readonly List<IDesignEventWithParameters> _designEventsWithParameters;
        private readonly List<IEcommerceEvent<IDataEventEcommerce>> _ecommerceEvents;

        private AnalyticsConfig _config;

        public EventsService()
        {
            _analyticsAdImpression = new List<IAdImpressionEvent<IDataEventAdImpression>>()
            {
                new FireBaseEvent(),
            };

            _analyticsCurrencies = new List<ICurrencyEvent<IDataEventCurrency>>()
            {
                new GameEvent(),
                new FireBaseEvent()
            };

            _designEvents = new List<IDesignEvent<IDataEventDesign>>()
            {
                new GameEvent(),
                new FireBaseEvent()
            };

            _designEventsWithParameters = new List<IDesignEventWithParameters>()
            {
                new FireBaseEvent()
            };

            _ecommerceEvents = new List<IEcommerceEvent<IDataEventEcommerce>>()
            {
                new GameEvent()
            };
            
            _config = new AnalyticsConfigFactory().Create();
        }

        public void SpendVirtualCurrency(DataEventCurrency dataEventCurrency, EventsServiceType flags = EventsServiceType.Everything)
        {
            foreach (var analyticsCurrency in FilterEventSystems(_analyticsCurrencies, flags))
            {
                analyticsCurrency.SpendVirtualCurrency(new DataEventCurrency(dataEventCurrency.ResourceId,
                    dataEventCurrency.CountResources,
                    dataEventCurrency.Type,
                    dataEventCurrency.PlaceId));
            }
        }

        public void EarnVirtualCurrency(DataEventCurrency dataEventCurrency, EventsServiceType flags = EventsServiceType.Everything)
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
            foreach (var eventSystem in FilterEventSystems(_designEvents, flags))
            {
                eventSystem.DesignEvent(dataEventDesign);
            }
        }

        private void DesignEvent(DataEventDesign dataEventDesign, List<Type> excludeTypesEventServices, EventsServiceType flags)
        {
            foreach (var eventSystem in FilterEventSystems(_designEvents, flags))
            {
                if (excludeTypesEventServices.Contains(eventSystem.GetType()))
                    continue;

                eventSystem.DesignEvent(dataEventDesign);
            }
        }

        public void DesignEventWithParams(DataEventDesignWithParams dataEventDesignWithParams, EventsServiceType flags = EventsServiceType.Everything)
        {
            var listExcludeEventServices = new List<Type>();
            
            foreach (var eventSystem in FilterEventSystems(_designEventsWithParameters, flags))
            {
                eventSystem.DesignEventWithParameters(dataEventDesignWithParams);

                listExcludeEventServices.Add(eventSystem.GetType());
            }

            DesignEvent(dataEventDesignWithParams, listExcludeEventServices, flags);
        }

        private List<T> FilterEventSystems<T>(List<T> systems, EventsServiceType flags = EventsServiceType.Everything)
        {
            var mask = _config.EnabledServices & flags;
            
            var clone = systems.ToList();

            if (!mask.HasFlag(EventsServiceType.Firebase)) clone.RemoveAll(s => s is FireBaseEvent);
            if (!mask.HasFlag(EventsServiceType.GA)) clone.RemoveAll(s => s is GameEvent);

            return clone;
        }

        public void AdRevenuePaidEvent(IDataEventAdImpression data)
        {
            foreach (var analytics in _analyticsAdImpression)
            {
                analytics.AdRevenuePaidEvent(new DataEventAdImpression(
                    data.AdSource,
                    data.AdFormat,
                    data.AdUnitName,
                    data.Currency,
                    data.Value));
            }
        }

        public void EcommercePurchase(IDataEventEcommerce dataEventEcommerce)
        {
            foreach (var e in _ecommerceEvents)
            {
                e.EcommercePurchase(dataEventEcommerce);
            }
        }
    }
}