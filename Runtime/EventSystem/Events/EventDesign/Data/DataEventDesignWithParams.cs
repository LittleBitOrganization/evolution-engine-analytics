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

        public override string ToString()
        {
            string parameters = "";
            foreach (var eventParameter in EventParameters)
            {
                parameters += $"{eventParameter.Name}: {eventParameter.ConvertValueToString()}\n";
            }

            return $"{base.ToString()}\n" +
                   "params:\n" +
                   "{parameters}";
                   
        }
    }
}