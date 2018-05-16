using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Exceptions
{
    [Serializable]
    public class BackOffStrategyFailedException : Exception
    {
        public BackOffStrategyFailedException() { }
        public BackOffStrategyFailedException(string message) : base(message) { }
        public BackOffStrategyFailedException(string message, Exception inner) : base(message, inner) { }
        protected BackOffStrategyFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
