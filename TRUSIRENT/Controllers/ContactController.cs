using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TRUSIRENT.Models;
using TRUSIRENT.Models.Entities;
using YourApp.Models;

namespace TRUSIRENT.Controllers
{
    public class SurveyController : Controller
    {
        private readonly TrusiRentDbContext _context;

        public SurveyController(TrusiRentDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var surveys = await _context.Surveys.ToListAsync();
            return View(surveys);
        }

        [Authorize(Roles = "Ankieter")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Ankieter")]
        [HttpPost]
        public async Task<IActionResult> Create(Survey survey, List<string> options)
        {
            if (!ModelState.IsValid)
                return View(survey);

            _context.Surveys.Add(survey);
            await _context.SaveChangesAsync();

            foreach (var option in options.Where(o => !string.IsNullOrWhiteSpace(o)))
            {
                _context.Options.Add(new Option
                {
                    Text = option,
                    SurveyId = survey.SurveyId
                });
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Options)
                .FirstOrDefaultAsync(s => s.SurveyId == id);

            if (survey == null)
                return NotFound();

            return View(survey);
        }

        [Authorize]
        public async Task<IActionResult> Vote(int optionId)
        {
            var option = await _context.Options.FindAsync(optionId);

            if (option == null)
                return NotFound();

            var vote = new Vote
            {
                OptionId = optionId,
                UserId = User.Identity!.Name!
            };

            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return RedirectToAction("Results", new { id = option.SurveyId });
        }

        public async Task<IActionResult> Results(int id)
        {
            var survey = await _context.Surveys
                .Include(s => s.Options)
                .ThenInclude(o => o.Votes)
                .FirstOrDefaultAsync(s => s.SurveyId == id);

            if (survey == null)
                return NotFound();

            return View(survey);
        }
    }
}