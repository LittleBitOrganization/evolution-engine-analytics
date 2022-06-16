using UnityEngine;

namespace LittleBit.Modules.Analytics.Initializers
{
    public class AnalyticsInitializer : MonoBehaviour, IInitializer
    {
        public void Start()
        {
            new FirebaseInitializer();
            new AdjustInitializer();
            new GameanalyticsInitializer();
        }
    }
}