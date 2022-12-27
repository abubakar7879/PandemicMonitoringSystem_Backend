using Microsoft.VisualBasic;
using System;

namespace PandemicMonitoringSystem.Models
{
    public class Address
    {
        public int Id { get; set; }


        public Char Street { get; set; }
        public String Area { get; set; }
        public String City { get; set; }
    }
}
