using MQ.CleaningRobot.Exceptions;
using System;

namespace MQ.CleaningRobot.Models
{
    public class Battery
    {
        public Battery(int initialLevel)
        {
            if (initialLevel < 0)
            {
                throw new ArgumentException("Battery level cannot be lower than 0.");
            }

            Level = initialLevel;
        }

        public int Level { get; private set; }

        public bool HasCapacity
        {
            get { return Level > 0; }
        }        

        public void Drain(int amount)
        {
            if (Level - amount < 0)
            {
                throw new OutOfBatteryException();
            }

            Level -= amount;
        }
    }
}