﻿using System;
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
            (new AdjustInitializer(gameObject)).Start();
            (new GameanalyticsInitializer()).Start();
            (new AmplitudeInitializer()).Start();
            (new WazzitudeInitializer()).Start();
            (new AppsFlyerInitializer()).Start();
        }
    }
}