using System;
using System.Collections.Generic;
using System.Text;

namespace MQ.CleaningRobot.Exceptions
{

    [Serializable]
    public class CollisionException : Exception
    {
        public CollisionException() { }
        public CollisionException(string message) : base(message) { }
        public CollisionException(string message, Exception inner) : base(message, inner) { }
        protected CollisionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
