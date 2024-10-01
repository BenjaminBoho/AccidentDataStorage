namespace AccidentDataStorage.Models.Accidents
{
    public class AccidentFilterViewModel
    {
        public string? ConstructionField { get; set; }
        public string? ConstructionType { get; set; }
        public string? AccidentBackground { get; set; }
        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }
        public int? MonthFrom { get; set; }
        public int? MonthTo { get; set; }
        public int? TimeFrom { get; set; }
        public int? TimeTo { get; set; }
        public string? SortOrder { get; set; }
    }
}
