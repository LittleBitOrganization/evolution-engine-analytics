namespace LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data
{
    public class DataEventDesign : IDataEventDesign
    {
        public string Label => _label;
        
        private readonly string _label;

        public DataEventDesign(string label)
        {
            _label = label;
        }
    }
}