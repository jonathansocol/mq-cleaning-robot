using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Exceptions
{
    [Serializable]
    public class OutOfBatteryException : Exception
    {
        public OutOfBatteryException() { }
        public OutOfBatteryException(string message) : base(message) { }
        public OutOfBatteryException(string message, Exception inner) : base(message, inner) { }
        protected OutOfBatteryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
