using GameAnalyticsSDK;
using UnityEngine;

namespace LittleBit.Modules.Analytics
{
    public class AnalyticsInitializer : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
            GameAnalyticsILRD.SubscribeMaxImpressions();
        }
    }
}