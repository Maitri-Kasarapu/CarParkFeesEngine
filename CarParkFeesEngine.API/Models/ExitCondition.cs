using System;
using System.Collections.Generic;

namespace CarParkFeesEngine.API.Models
{
    public class ExitCondition
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<string> ParkingDays { get; set; }
    } 
}
