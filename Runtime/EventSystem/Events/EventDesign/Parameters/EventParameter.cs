using System;
using System.Collections.Generic;

namespace LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters
{
    public class EventParameter
    {
        private string _name;
        private double _valueDouble;
        private long _valueLong;
        private string _valueString;
        private Type _typeValue;
        
        public Type TypeValue => _typeValue;

        public string Name => _name;

        public double ValueDouble => _valueDouble;

        public long ValueLong => _valueLong;

        public string ValueString => _valueString;
        
        
        public EventParameter(string parameterName, double parameterValue)
        {
            _name = parameterName;
            _valueDouble = parameterValue;
            _typeValue = parameterValue.GetType();
        }

        public EventParameter(string parameterName, long parameterValue)
        {
            _name = parameterName;
            _valueLong = parameterValue;
            _typeValue = parameterValue.GetType();
        }

        public EventParameter(string parameterName, string parameterValue)
        {
            _name = parameterName;
            _valueString = parameterValue;
            _typeValue = parameterValue.GetType();
        }

        public static EventParameter[] operator + (EventParameter parameterLeft, EventParameter parameterRight)
        {
            return new[] {parameterLeft, parameterRight};
        }
        
        public static EventParameter[] operator + (EventParameter [] parameterLeft, EventParameter parameterRight)
        {
            List<EventParameter> eventParameters = new List<EventParameter>();
            eventParameters.AddRange(parameterLeft);
            eventParameters.Add(parameterRight);
            return eventParameters.ToArray();
        }
        
        public static EventParameter[] operator + (EventParameter parameterLeft, EventParameter [] parameterRight)
        {
            List<EventParameter> eventParameters = new List<EventParameter>();
            eventParameters.Add(parameterLeft);
            eventParameters.AddRange(parameterRight);
            return eventParameters.ToArray();
        }
        



    }
}