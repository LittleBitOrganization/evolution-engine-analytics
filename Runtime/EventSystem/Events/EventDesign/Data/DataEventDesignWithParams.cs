using LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters;

namespace LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Data
{
    public class DataEventDesignWithParams : DataEventDesign
    {
        private readonly EventParameter[] _eventParameters;

        public EventParameter[] EventParameters => _eventParameters;

        public DataEventDesignWithParams(string label, params EventParameter[] eventParameters) : base(label)
        {
            _eventParameters = eventParameters;
        }
    }
}