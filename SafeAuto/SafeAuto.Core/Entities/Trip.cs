using System;

namespace SafeAuto.Core.Entities
{
    public class Trip
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan StopTime { get; set; }
        public double MilesDriven { get; set; }

        public double Speed { get; set; }
    }
}
