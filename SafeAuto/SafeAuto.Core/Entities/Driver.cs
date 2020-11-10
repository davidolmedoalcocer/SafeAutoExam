using System.Collections.Generic;

namespace SafeAuto.Core.Entities
{
    public class Driver
    {
        public string Name { get; set; }
        public double Distance { get; set; }

        public double SpeedAverage { get; set; }

        public List<Trip> Trips { get; set; }
    }
}
