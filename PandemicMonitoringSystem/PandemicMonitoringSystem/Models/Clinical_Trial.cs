namespace PandemicMonitoringSystem.Models
{
    public class Clinical_Trial
    {
        public int id { get; set; }
        public int type { get; set; }
        public int people_taking_part { get; set; }
        public int recovered { get; set; }
        public int fatalities { get; set; }

    }
}
