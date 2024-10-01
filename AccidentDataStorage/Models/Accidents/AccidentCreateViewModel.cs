namespace AccidentDataStorage.Models.Accidents
{
    public class AccidentCreateViewModel
    {
        public int? AccidentId { get; set; }
        public string ConstructionField { get; set; }
        public string ConstructionType { get; set; }
        public string WorkType { get; set; }
        public string ConstructionMethod { get; set; }
        public string DisasterCategory { get; set; }
        public string AccidentCategory { get; set; }
        public string? Weather { get; set; }
        public int AccidentYear { get; set; }
        public int AccidentMonth { get; set; }
        public int AccidentDateTime { get; set; }
        public string AccidentLocationPref { get; set; }
        public string? AccidentBackground { get; set; }
        public string? AccidentCause { get; set; }
        public string? AccidentCountermeasure { get; set; }
    }

}
