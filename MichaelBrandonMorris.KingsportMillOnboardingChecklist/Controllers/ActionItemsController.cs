using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Data;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models.ActionItemViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Controllers
{
    public class ActionItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActionItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var policies = await _context.Policies.ToListAsync();

            ViewBag.PolicySelectList = policies.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            });

            var users = await _context.Users.Where(u => u.IsAsignee).ToListAsync();

            ViewBag.AsigneeSelectList = users.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var asignee = await _context.Users.FindAsync(model.AssigneeId);
            var policy = await _context.Policies.FindAsync(model.PolicyId);
            var actionItem = ActionItem.Create(model, asignee, policy);
            _context.Add(actionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionItem =
                await _context.ActionItems
                    .SingleOrDefaultAsync(m => m.Id == id);
            if (actionItem == null)
            {
                return NotFound();
            }

            return View(actionItem);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actionItem =
                await _context.ActionItems
                    .SingleOrDefaultAsync(m => m.Id == id);
            _context.ActionItems.Remove(actionItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionItem =
                await _context.ActionItems
                    .SingleOrDefaultAsync(m => m.Id == id);
            if (actionItem == null)
            {
                return NotFound();
            }

            return View(actionItem);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actionItem =
                await _context.ActionItems
                    .SingleOrDefaultAsync(m => m.Id == id);
            if (actionItem == null)
            {
                return NotFound();
            }

            return View(actionItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind(
                "CompletedOn,CreatedOn,Id,Name,OffboardingDescription,OnboardingDescription,Type")]
            ActionItem actionItem)
        {
            if (id != actionItem.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(actionItem);
            }

            try
            {
                _context.Update(actionItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionItemExists(actionItem.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActionItems.ToListAsync());
        }

        private bool ActionItemExists(int id)
        {
            return _context.ActionItems.Any(e => e.Id == id);
        }
    }
}