using System.Threading.Tasks;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Models.
    UserViewModels;
using MichaelBrandonMorris.KingsportMillOnboardingChecklist.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MichaelBrandonMorris.KingsportMillOnboardingChecklist.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(
            IEmailSender emailSender,
            UserManager<User> userManager)
        {
            EmailSender = emailSender;
            UserManager = userManager;
        }

        private IEmailSender EmailSender
        {
            get;
        }

        private UserManager<User> UserManager
        {
            get;
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(
            string id,
            ChangePasswordViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await UserManager.FindByIdAsync(id);
            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, model.NewPassword);
            await UserManager.UpdateAsync(user);

            await EmailSender.SendEmailAsync(
                user.Email,
                "Kingsport Mill Onboarding Checklist - Password Reset",
                $"Your password has been changed for you by an administrator.<br />User name: {user.Email}<br />Password: {model.NewPassword}");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = Models.User.Create(model);
            var result = await UserManager.CreateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            await UserManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            [Bind(
                "EmployeeId,FirstName,LastName,IsAsignee,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")]
            User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await UserManager.UpdateAsync(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(user.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await UserManager.Users.ToListAsync();
            return View(model);
        }

        private async Task<bool> UserExists(string id)
        {
            return await UserManager.FindByIdAsync(id) != null;
        }
    }
}