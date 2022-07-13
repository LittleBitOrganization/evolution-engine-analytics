using System;
using Firebase;
using LittleBit.Modules.Analytics.EventSystem.Services;
using LittleBitGames.Environment.Events;
using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class FirebaseInitializer : IInitializer
    {
        public void Start ()
        {
            var analyticsConfig = new AnalyticsConfigFactory().Create();
            if (analyticsConfig.IsEnableService(EventsServiceType.Firebase))
            {
                FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        // Create and hold a reference to your FirebaseApp,
                        // where app is a Firebase.FirebaseApp property of your application class.
                        // Crashlytics will use the DefaultInstance, as well;
                        // this ensures that Crashlytics is initialized.
                        FirebaseApp app = FirebaseApp.DefaultInstance;
                        FirebaseApp.LogLevel = LogLevel.Debug;
                        // Set a flag here for indicating that your project is ready to use Firebase.
                    }
                    else
                    {
                        Debug.LogError(String.Format(
                            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                        // Firebase Unity SDK is not safe to use here.
                    }
                });
            }
        }
    }
}
