using AccidentDataStorage.Data;
using AccidentDataStorage.Models.Accidents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccidentDataStorage.Controllers
{
    [Authorize]
    public class AccidentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AccidentService _accidentService = new();

        public AccidentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Accidents
        public async Task<IActionResult> Index(string constructionField, string constructionType, int? yearFrom, int? monthFrom, int? timeFrom, int? yearTo, int? monthTo, int? timeTo, string accidentBackground, string sortOrder)
        {
            TimeDropDowns();
            PopulateSortingViewBags(sortOrder);

            var constructionFields = await _context.ItemList
                .Where(i => i.ItemGenre == "ConstructionField")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToListAsync();
            ViewBag.ConstructionFieldList = new SelectList(constructionFields, "ItemName", "ItemName", constructionField);

            var constructionTypes = await _context.ItemList
                .Where(i => i.ItemGenre == "ConstructionType")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToListAsync();
            ViewBag.ConstructionTypeList = new SelectList(constructionTypes, "ItemName", "ItemName", constructionType);

            var accidents = _context.Accidents.AsQueryable();

            accidents = _accidentService.FilterAccidents(accidents, constructionField, constructionType, accidentBackground, yearFrom, yearTo, monthFrom, monthTo, timeFrom, timeTo);

            if (string.IsNullOrEmpty(sortOrder))
            {
                accidents = accidents.OrderByDescending(a => a.AccidentId);
            }
            else
            {
                accidents = _accidentService.SortAccidents(accidents, sortOrder);
            }

            var parsedAccidents = await accidents.ToListAsync();

            if (!parsedAccidents.Any())
            {
                ViewBag.ErrorMessage = "指定された検索条件に当てはまるデータは存在しませんでした";
            }

            return View(parsedAccidents);
        }

        // GET: Accidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accidents = await _context.Accidents
                .FirstOrDefaultAsync(m => m.AccidentId == id);
            if (accidents == null)
            {
                return NotFound();
            }

            return View(accidents);
        }

        // GET: Accidents/Create
        public IActionResult Create()
        {
            TimeDropDowns();
            PopulateDropDowns();

            return View();
        }

        // POST: Accidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccidentId,ConstructionField,ConstructionType,WorkType,ConstructionMethod,DisasterCategory,AccidentCategory,Weather,AccidentYear,AccidentMonth,AccidentDateTime,AccidentLocationPref,AccidentBackground,AccidentCause,AccidentCountermeasure")] Accidents accidents)
        {

            TimeDropDowns();
            PopulateDropDowns();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(accidents);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { success = true });
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "保存失敗しました");
                }
            }

            ViewBag.FailureFlag = true;
            return View(accidents);
        }

        // GET: Accidents/Edit/5
        public async Task<IActionResult> Edit(int? id, string? constructionField, string? constructionType, int? yearFrom, int? monthFrom, int? timeFrom, int? yearTo, int? monthTo, int? timeTo, string? accidentBackground)
        {
            TimeDropDowns();
            PopulateDropDowns();

            ViewBag.ConstructionField = constructionField;
            ViewBag.ConstructionType = constructionType;
            ViewBag.YearFrom = yearFrom;
            ViewBag.MonthFrom = monthFrom;
            ViewBag.TimeFrom = timeFrom;
            ViewBag.YearTo = yearTo;
            ViewBag.MonthTo = monthTo;
            ViewBag.TimeTo = timeTo;
            ViewBag.AccidentBackground = accidentBackground;

            if (id == null)
            {
                return NotFound();
            }

            var accidents = await _context.Accidents.FindAsync(id);
            if (accidents == null)
            {
                return NotFound();
            }

            return View(accidents);
        }

        // POST: Accidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccidentId,ConstructionField,ConstructionType,WorkType,ConstructionMethod,DisasterCategory,AccidentCategory,Weather,AccidentYear,AccidentMonth,AccidentDateTime,AccidentLocationPref,AccidentBackground,AccidentCause,AccidentCountermeasure")] Accidents accidents, 
            string? FilterConstructionField,
            string? FilterConstructionType,
            int? FilterYearFrom,
            int? FilterMonthFrom,
            int? FilterTimeFrom,
            int? FilterYearTo,
            int? FilterMonthTo,
            int? FilterTimeTo,
            string? FilterAccidentBackground)
        {
            TimeDropDowns();
            PopulateDropDowns();

            if (id != accidents.AccidentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accidents);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new
                    {
                        success = true,
                        constructionField = FilterConstructionField,
                        constructionType = FilterConstructionType,
                        yearFrom = FilterYearFrom,
                        monthFrom = FilterMonthFrom,
                        timeFrom = FilterTimeFrom,
                        yearTo = FilterYearTo,
                        monthTo = FilterMonthTo,
                        timeTo = FilterTimeTo,
                        accidentBackground = FilterAccidentBackground
                    });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccidentsExists(accidents.AccidentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "保存失敗しました");
                    }
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "保存失敗しました");
                }
            }

            ViewBag.ConstructionField = FilterConstructionField;
            ViewBag.ConstructionType = FilterConstructionType;
            ViewBag.YearFrom = FilterYearFrom;
            ViewBag.MonthFrom = FilterMonthFrom;
            ViewBag.TimeFrom = FilterTimeFrom;
            ViewBag.YearTo = FilterYearTo;
            ViewBag.MonthTo = FilterMonthTo;
            ViewBag.TimeTo = FilterTimeTo;
            ViewBag.AccidentBackground = FilterAccidentBackground;

            ViewBag.FailureFlag = true;
            return View(accidents);
        }

        // GET: Accidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accidents = await _context.Accidents
                .FirstOrDefaultAsync(m => m.AccidentId == id);
            if (accidents == null)
            {
                return NotFound();
            }

            return View(accidents);
        }


        // POST: Accidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accidents = await _context.Accidents.FindAsync(id);
            if (accidents != null)
            {
                _context.Accidents.Remove(accidents);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AccidentsExists(int id)
        {
            return _context.Accidents.Any(e => e.AccidentId == id);
        }

        private void TimeDropDowns()
        {
            ViewBag.YearList = new SelectList(Enumerable.Range(2010, DateTime.Now.Year - 2010 + 1));
            ViewBag.MonthList = new SelectList(Enumerable.Range(1, 12));
            ViewBag.TimeList = new SelectList(Enumerable.Range(0, 24));
        }

        // Helper method to populate dropdown lists
        private void PopulateDropDowns()
        {
            // 工事分野 (ConstructionField)
            var constructionFields = _context.ItemList
                .Where(i => i.ItemGenre == "ConstructionField")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.ConstructionFieldList = constructionFields;

            // 工事の種類 (ConstructionType)
            var constructionTypes = _context.ItemList
                .Where(i => i.ItemGenre == "ConstructionType")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.ConstructionTypeList = constructionTypes;

            // 工種 (WorkType)
            var workTypes = _context.ItemList
                .Where(i => i.ItemGenre == "WorkType")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.WorkTypeList = workTypes;

            // 工法・形式名 (ConstructionMethod)
            var constructionMethods = _context.ItemList
                .Where(i => i.ItemGenre == "ConstructionMethod")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.ConstructionMethodList = constructionMethods;

            // 災害分類 (DisasterCategory)
            var disasterCategories = _context.ItemList
                .Where(i => i.ItemGenre == "DisasterCategory")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.DisasterCategoryList = disasterCategories;

            // 事故分類 (AccidentCategory)
            var accidentCategories = _context.ItemList
                .Where(i => i.ItemGenre == "AccidentCategory")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.AccidentCategoryList = accidentCategories;

            // 天候 (Weather)
            var weathers = _context.ItemList
                .Where(i => i.ItemGenre == "Weather")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.WeatherList = weathers;

            // 事故発生場所 (AccidentLocationPref)
            var accidentLocations = _context.ItemList
                .Where(i => i.ItemGenre == "AccidentLocationPref")
                .Select(i => new { i.ItemValue, i.ItemName })
                .ToList();
            ViewBag.AccidentLocationPrefList = accidentLocations;
        }

        // Helper method to handle sorting ViewBags
        private void PopulateSortingViewBags(string sortOrder)
        {
            ViewBag.AccidentIdSort = sortOrder == "AccidentId" ? "AccidentId_desc" : "AccidentId";
            ViewBag.ConstructionFieldSort = sortOrder == "ConstructionField" ? "ConstructionField_desc" : "ConstructionField";
            ViewBag.ConstructionTypeSort = sortOrder == "ConstructionType" ? "ConstructionType_desc" : "ConstructionType";
            ViewBag.WorkTypeSort = sortOrder == "WorkType" ? "WorkType_desc" : "WorkType";
            ViewBag.ConstructionMethodSort = sortOrder == "ConstructionMethod" ? "ConstructionMethod_desc" : "ConstructionMethod";
            ViewBag.DisasterCategorySort = sortOrder == "DisasterCategory" ? "DisasterCategory_desc" : "DisasterCategory";
            ViewBag.AccidentCategorySort = sortOrder == "AccidentCategory" ? "AccidentCategory_desc" : "AccidentCategory";
            ViewBag.WeatherSort = sortOrder == "Weather" ? "Weather_desc" : "Weather";
            ViewBag.AccidentDateSort = sortOrder == "AccidentDate" ? "AccidentDate_desc" : "AccidentDate";
            ViewBag.AccidentLocationPrefSort = sortOrder == "AccidentLocationPref" ? "AccidentLocationPref_desc" : "AccidentLocationPref";
            ViewBag.AccidentBackgroundSort = sortOrder == "AccidentBackground" ? "AccidentBackground_desc" : "AccidentBackground";
        }
    }
}


