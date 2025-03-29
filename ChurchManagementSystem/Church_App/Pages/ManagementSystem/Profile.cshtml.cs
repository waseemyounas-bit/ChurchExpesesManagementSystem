using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using System.ComponentModel.DataAnnotations;

namespace Church_App.Pages.ManagementSystem
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthService _authService;
        public ProfileModel(IHttpContextAccessor httpContextAccessor, AuthService authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }
        [BindProperty]
        public ProfileUpdateViewModel ProfileInput { get; set; } = new ProfileUpdateViewModel();


        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Compare("profileuser.PasswordHash", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
        public async Task OnGetAsync()
        {
            var userEmail = _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
            var user = await _authService.GetUserByEmailAsync(userEmail);

            if (user != null)
            {
                ProfileInput = new ProfileUpdateViewModel
                {
                    FirstName = user.FirstName,
                    Email = user.Email
                };
            }
        }

        public async Task<IActionResult> OnPostUpdateprofileAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _authService.GetUserByEmailAsync(ProfileInput.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                return Page();
            }

            user.FirstName = ProfileInput.FirstName;
            user.FullName = ProfileInput.FirstName;

            var result = await _authService.UpdateUserAsync(user, ProfileInput.NewPassword);

            if (result.Succeeded)
            {
                TempData["Success"] = "Profile updated successfully.";
                return RedirectToPage("/ManagementSystem/Profile");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }


    }
}
