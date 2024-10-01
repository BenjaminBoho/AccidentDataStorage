namespace AccidentDataStorage.Models.Accidents
{
    public class AccidentIndexViewModel
    {
        public AccidentFilterViewModel Filter { get; set; } = new AccidentFilterViewModel();
        public IEnumerable<Accidents> Accidents { get; set; } = new List<Accidents>();
    }

}
