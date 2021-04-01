namespace CarParkFeesEngine.API.Models
{
    public class PackageRate
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double TotalPrice { get; set; }
        public EntryCondition EntryCondition { get; set; }
        public ExitCondition ExitCondition { get; set; }
        public int MaxDaysAllowed { get; set; }
    }

}
