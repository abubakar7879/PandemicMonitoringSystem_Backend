using System;

namespace PandemicMonitoringSystem.Models
{
    public class Infected
    {
        public int person_id { get; set; }
        public int symptoms { get; set; }
        public DateTime Date_of_infection { get; set; }
        public int center_id { get; set; }
    }
}
