using GameAnalyticsSDK;
using UnityEngine;

namespace LittleBit.Modules.Analytics
{
    public class GameanalyticsInitializer : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
            GameAnalyticsILRD.SubscribeMaxImpressions();
        }
    }
}