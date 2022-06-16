namespace LittleBit.Modules.Analytics.EventSystem
{
    public class SdkSource
    {
        public string Source { get; private set; }

        public SdkSource(string source)
        {
            Source = source;
        }
    }
}