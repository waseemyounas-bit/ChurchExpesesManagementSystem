using Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public LoginviewModel LoginData { get; set; } = new();

        public string ErrorMessage { get; set; }

        public IndexModel(AuthService authService, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _authService = authService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            bool isAuthenticated = await _authService.LoginAsync(LoginData.UsernameOrEmail, LoginData.Password);

            if (isAuthenticated)
            {
                return RedirectToPage("/Dashboard"); // Change to your dashboard page
            }

            ErrorMessage = "Invalid login credentials. Please try again.";
            return Page();

        }

    }
}
