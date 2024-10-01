using System.Linq;
using AccidentDataStorage.Models.Accidents;

public class AccidentService
{
    public IQueryable<Accidents> FilterAccidents(IQueryable<Accidents> accidents, string constructionField, string constructionType, string accidentBackground, int? yearFrom, int? yearTo, int? monthFrom, int? monthTo, int? timeFrom, int? timeTo)
    {
        if (!string.IsNullOrEmpty(constructionField))
        {
            accidents = accidents.Where(a => a.ConstructionField == constructionField);
        }
        if (!string.IsNullOrEmpty(constructionType))
        {
            accidents = accidents.Where(a => a.ConstructionType == constructionType);
        }
        if (!string.IsNullOrEmpty(accidentBackground))
        {
            accidents = accidents.Where(a => a.AccidentBackground.Contains(accidentBackground));
        }

        if (yearFrom.HasValue && yearTo.HasValue)
        {
            accidents = accidents.Where(a => a.AccidentYear >= yearFrom && a.AccidentYear <= yearTo);
        }

        if (monthFrom.HasValue && monthTo.HasValue)
        {
            accidents = accidents.Where(a => a.AccidentMonth >= monthFrom && a.AccidentMonth <= monthTo);
        }

        if (timeFrom.HasValue && timeTo.HasValue)
        {
            accidents = accidents.Where(a => a.AccidentDateTime >= timeFrom && a.AccidentDateTime <= timeTo);
        }

        return accidents;
    }

    public IQueryable<Accidents> SortAccidents(IQueryable<Accidents> accidents, string sortOrder)
    {
        switch (sortOrder)
        {
            case "AccidentId":
                return accidents.OrderBy(a => a.AccidentId);
            case "AccidentId_desc":
                return accidents.OrderByDescending(a => a.AccidentId);

            case "ConstructionField":
                return accidents.OrderBy(a => a.ConstructionField == null)
                                .ThenBy(a => a.ConstructionField);
            case "ConstructionField_desc":
                return accidents.OrderByDescending(a => a.ConstructionField == null)
                                .ThenByDescending(a => a.ConstructionField);

            case "ConstructionType":
                return accidents.OrderBy(a => a.ConstructionType == null)
                                .ThenBy(a => a.ConstructionType);
            case "ConstructionType_desc":
                return accidents.OrderByDescending(a => a.ConstructionType == null)
                                .ThenByDescending(a => a.ConstructionType);

            case "WorkType":
                return accidents.OrderBy(a => a.WorkType == null)
                                .ThenBy(a => a.WorkType);
            case "WorkType_desc":
                return accidents.OrderByDescending(a => a.WorkType == null)
                                .ThenByDescending(a => a.WorkType);

            case "ConstructionMethod":
                return accidents.OrderBy(a => a.ConstructionMethod == null)
                                .ThenBy(a => a.ConstructionMethod);
            case "ConstructionMethod_desc":
                return accidents.OrderByDescending(a => a.ConstructionMethod == null)
                                .ThenByDescending(a => a.ConstructionMethod);

            case "DisasterCategory":
                return accidents.OrderBy(a => a.DisasterCategory == null)
                                .ThenBy(a => a.DisasterCategory);
            case "DisasterCategory_desc":
                return accidents.OrderByDescending(a => a.DisasterCategory == null)
                                .ThenByDescending(a => a.DisasterCategory);

            case "AccidentCategory":
                return accidents.OrderBy(a => a.AccidentCategory == null)
                                .ThenBy(a => a.AccidentCategory);
            case "AccidentCategory_desc":
                return accidents.OrderByDescending(a => a.AccidentCategory == null)
                                .ThenByDescending(a => a.AccidentCategory);

            case "Weather":
                return accidents.OrderBy(a => a.Weather == null)
                                .ThenBy(a => a.Weather);
            case "Weather_desc":
                return accidents.OrderByDescending(a => a.Weather == null)
                                .ThenByDescending(a => a.Weather);

            case "AccidentDate":
                return accidents.OrderBy(a => a.AccidentYear)
                                .ThenBy(a => a.AccidentMonth)
                                .ThenBy(a => a.AccidentDateTime);
            case "AccidentDate_desc":
                return accidents.OrderByDescending(a => a.AccidentYear)
                                .ThenByDescending(a => a.AccidentMonth)
                                .ThenByDescending(a => a.AccidentDateTime);

            case "AccidentLocationPref":
                return accidents.OrderBy(a => a.AccidentLocationPref == null)
                                .ThenBy(a => a.AccidentLocationPref);
            case "AccidentLocationPref_desc":
                return accidents.OrderByDescending(a => a.AccidentLocationPref == null)
                                .ThenByDescending(a => a.AccidentLocationPref);

            case "AccidentBackground":
                return accidents.OrderBy(a => a.AccidentBackground == null)
                                .ThenBy(a => a.AccidentBackground);
            case "AccidentBackground_desc":
                return accidents.OrderByDescending(a => a.AccidentBackground == null)
                                .ThenByDescending(a => a.AccidentBackground);

            default:
                return accidents.OrderByDescending(a => a.AccidentId);
        }
    }
}
