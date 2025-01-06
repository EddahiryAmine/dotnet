// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmsiStudySpace.Models;
using Microsoft.EntityFrameworkCore;

namespace EmsiStudySpace.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger , UserManager<ApplicationUser> userManager, AppDbContext dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Mettre à jour le champ LastLogoutTime
                user.LastLogoutTime = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
                var lastHistory = _dbContext.UserConnectionHistories
           .Where(h => h.UserId == user.Id && h.LogoutTime == null)
           .OrderByDescending(h => h.LoginTime)
           .FirstOrDefault();

                if (lastHistory != null)
                {
                    lastHistory.LogoutTime = DateTime.UtcNow;
                    _dbContext.UserConnectionHistories.Update(lastHistory);
                    await _dbContext.SaveChangesAsync();
                }
            }

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
