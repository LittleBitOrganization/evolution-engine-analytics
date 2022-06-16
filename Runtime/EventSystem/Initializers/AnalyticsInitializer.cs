using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AnalyticsInitializer : MonoBehaviour, IInitializer
    {
        public void Start()
        {
            (new FirebaseInitializer()).Start();
            (new AdjustInitializer(gameObject)).Start();
            (new GameanalyticsInitializer()).Start();
        }
    }
}