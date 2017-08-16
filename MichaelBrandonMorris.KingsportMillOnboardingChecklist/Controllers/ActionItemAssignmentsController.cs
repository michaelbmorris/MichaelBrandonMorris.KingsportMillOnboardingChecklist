using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Controllers
{
    public class ActionItemAssignmentsController : Controller
    {
        public ApplicationDbContext Context
        {
            get;
        }

        public ActionItemAssignmentsController(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await Context.ActionItemAssignments.Include(a => a.ActionItem).ThenInclude(a => a.Assignee).ToListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            var actionItemAssignment = await Context.ActionItemAssignments
                .Include(a => a.ActionItem)
                .ThenInclude(a => a.Assignee)
                .FirstOrDefaultAsync(a => a.Id == id);

            return View(actionItemAssignment);
        }
    }
}