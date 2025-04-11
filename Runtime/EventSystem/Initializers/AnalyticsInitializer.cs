using System;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AnalyticsInitializer : MonoBehaviour, IInitializer
    {
        public event Action<bool> OnFirebaseInit;
        
        public void Start()
        {
            var firebaseInitializer = new FirebaseInitializer();
            firebaseInitializer.OnFirebaseInit += b => OnFirebaseInit?.Invoke(b);
            firebaseInitializer.Start();
            (new GameanalyticsInitializer()).Start();

            var analyticsConfig = new AnalyticsConfigFactory().Create();

            foreach (var analyticsServiceConfig in analyticsConfig.AdditionalServiceConfig)
            {
                analyticsServiceConfig.CreateInitializer()?.Start();
            }
        }
    }
}