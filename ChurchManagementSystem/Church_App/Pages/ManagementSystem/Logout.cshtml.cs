using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class LogoutModel : PageModel
    {
        private readonly AuthService _authService;

        public LogoutModel(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _authService.LogoutAsync(); // Sign out from Identity
            HttpContext.Session.Clear();      // Clear session data

            return RedirectToPage("/Index");  // Redirect to login
        }
    }
}
