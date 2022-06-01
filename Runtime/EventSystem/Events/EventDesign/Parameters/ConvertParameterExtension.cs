using System;
using Firebase.Analytics;

namespace LittleBit.Modules.Analytics.EventSystem.Events.EventDesign.Parameters
{
    public static class ConvertParameterExtension
    {
        public static Parameter ConvertToFirebaseParameter(this EventParameter e)
        {
            if (e.TypeValue == typeof(double))
            {
                return new Parameter(e.Name, e.ValueDouble);
            }
            else if (e.TypeValue == typeof(long))
            {
                return new Parameter(e.Name, e.ValueLong);
            }
            else if (e.TypeValue == typeof(string))
            {
                return new Parameter(e.Name, e.ValueString);
            }
            else
            {
                throw new Exception("Не определена реализация для данного типа данных");
            }
        }

        public static string ConvertValueToString(this EventParameter e)
        {
            if (e.TypeValue == typeof(double))
            {
                return e.ValueDouble.ToString();
            }
            else if (e.TypeValue == typeof(long))
            {
                return e.ValueLong.ToString();
            }
            else if (e.TypeValue == typeof(string))
            {
                return e.ValueString;
            }
            else
            {
                throw new Exception("Не определена реализация для данного типа данных");
            }
        }
    }
}