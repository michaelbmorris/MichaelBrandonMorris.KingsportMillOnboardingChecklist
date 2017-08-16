using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Data;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Controllers
{
    public class ChecklistController : Controller
    {
        public ChecklistController(
            ApplicationDbContext context,
            IEmailSender emailSender,
            UserManager<User> userManager)
        {
            Context = context;
            EmailSender = emailSender;
            UserManager = userManager;
        }

        public ApplicationDbContext Context
        {
            get;
        }

        public IEmailSender EmailSender
        {
            get;
        }

        public UserManager<User> UserManager
        {
            get;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var policies = await Context.Policies.Include(p => p.ActionItems)
                .ToListAsync();

            return View(policies);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            string id,
            IEnumerable<int> model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await UserManager.Users.Include(u => u.Checklist)
                .SingleOrDefaultAsync(u => u.Id == id);

            Checklist checklist;

            if (user.Checklist == null)
            {
                checklist = new Checklist();
                user.Checklist = checklist;
                Context.Update(user);
            }
            else
            {
                checklist = user.Checklist;
                Context.Entry(checklist).State = EntityState.Modified;
            }

            foreach (var item in model)
            {
                var actionItem = await Context.ActionItems.FindAsync(item);
                checklist.ActionItems.Add(actionItem);
            }

            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var checklist = await Context.Checklists
                .Include(c => c.User)
                .Include(c => c.ActionItems)
                .ThenInclude(a => a.Assignee)
                .FirstOrDefaultAsync(c => c.Id == id);

            return View(checklist);
        }

        public async Task<IActionResult> Index()
        {
            var checklists = await Context.Checklists.Include(c => c.User)
                .Include(c => c.ActionItems)
                .ToListAsync();

            return View(checklists);
        }

        [HttpGet]
        public async Task<IActionResult> Onboard(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await Context.Users.Include(u => u.ActionItemAssignments)
                .SingleOrDefaultAsync(u => u.Id == id);

            return View(user);
        }

        [ActionName("Onboard")]
        [HttpPost]
        public async Task<IActionResult> OnboardConfirm(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await UserManager.Users.Include(u => u.Checklist)
                .ThenInclude(c => c.ActionItems)
                .ThenInclude(a => a.Assignee)
                .SingleOrDefaultAsync(u => u.Id == id);

            foreach (var actionItem in user.Checklist.ActionItems.Where(
                a => a.Type == ActionItemType.Onboarding
                     || a.Type == ActionItemType.Both))
            {
                var actionItemAssignment = new ActionItemAssignment
                {
                    CreatedOn = DateTime.Now,
                    ActionItem = actionItem,
                    Id = Guid.NewGuid().ToString(),
                    IsOnboarding = true
                };

                user.ActionItemAssignments.Add(actionItemAssignment);

                await EmailSender.SendEmailAsync(
                    actionItem.Assignee.Email,
                    "Kingsport Mill Onboarding Checklist - Action Item Assigned",
                    $"A new action item has been assigned to you. Click <a href=\"{Url.Action("Details", "ActionItemAssignments", new { id = actionItemAssignment.Id })}\">here</a> to view.");
            }

            Context.Update(user);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}