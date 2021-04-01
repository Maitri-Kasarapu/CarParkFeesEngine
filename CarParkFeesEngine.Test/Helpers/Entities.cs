using CarParkFeesEngine.API.Models;
using System;
using System.Collections.Generic;

namespace CarParkFeesEngine.Test.Helpers
{
    public class Entities
    {
        public static List<PackageRate> GetSamplePackageRates()
        {
            return new List<PackageRate>
            {
                new PackageRate
                {
                    Name =  "Early Bird",
                    Type =  "Flat Rate",
                    TotalPrice =  13.00,
                    EntryCondition = new EntryCondition
                    {
                        StartTime =  new TimeSpan(6, 0, 0),
                        EndTime =  new TimeSpan(9, 0, 0),
                        ParkingDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" }
                    },
                    ExitCondition = new ExitCondition
                    {
                        StartTime =  new TimeSpan(15, 30, 0),
                        EndTime =  new TimeSpan(21, 30, 0),
                        ParkingDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" }
                    },
                    MaxDaysAllowed = 0
                },
                new PackageRate
                {
                    Name =  "Night Rate",
                    Type =  "Flat Rate",
                    TotalPrice =  6.50,
                    EntryCondition = new EntryCondition
                    {
                        StartTime =  new TimeSpan(18, 0, 0),
                        EndTime =  new TimeSpan(0, 0, 0),
                        ParkingDays = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" }
                    },
                    ExitCondition = new ExitCondition
                    {
                        StartTime =  new TimeSpan(18, 0, 0),
                        EndTime =  new TimeSpan(8, 0, 0),
                        ParkingDays = new List<string> { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" }
                    },
                    MaxDaysAllowed = 1
                },
                new PackageRate
                {
                    Name =  "WeekEndTime Rate",
                    Type =  "Flat Rate",
                    TotalPrice =  10,
                    EntryCondition = new EntryCondition
                    {
                        StartTime =  new TimeSpan(0, 0, 0),
                        EndTime =  new TimeSpan(0, 0, 0),
                        ParkingDays = new List<string> { "Sunday", "Saturday" }
                    },
                    ExitCondition = new ExitCondition
                    {
                        StartTime =  new TimeSpan(0, 0, 0),
                        EndTime =  new TimeSpan(0, 0, 0),
                        ParkingDays = new List<string> { "Sunday", "Monday", "Saturday" }
                    },
                    MaxDaysAllowed = 2
                }
            };               
  
        }

        public static List<HourlyRate> GetSampleHourlyRates()
        {
            return new List<HourlyRate>
            {
                new HourlyRate { MaxAllowedHours = 1, TotalPrice = 5.00 },
                new HourlyRate { MaxAllowedHours = 2, TotalPrice = 10.00 },
                new HourlyRate { MaxAllowedHours = 3, TotalPrice = 15.00 },
                new HourlyRate { MaxAllowedHours = 24, TotalPrice = 20.00 }
            };

        }
    }
}
