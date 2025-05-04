using BoldReports.Processing.ObjectModels;
using DataAccess.Data;
using Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AuthService _authService;
        private readonly IChurchSettingService _churchsettingservice;
        private readonly IChurchSettingService _churchSettingService;
        private readonly ILogger<IndexModel> _logger;
        private readonly DataContext _context;

        [BindProperty]
        public LoginviewModel LoginData { get; set; } = new();

        public string Logopath { get; set; } = "";
        public string ChurchName { get; set; } = "";
        public string ErrorMessage { get; set; }

        public IndexModel(AuthService authService, ILogger<IndexModel> logger, IChurchSettingService churchSettingService, IChurchSettingService churchsettingservice, DataContext dataContext)
        {
            _logger = logger;
            _authService = authService;
            _churchSettingService = churchSettingService;
            _churchsettingservice = churchsettingservice;
            _context = dataContext;
        }

        public void OnGet()
        {
            var churchSetting = _churchSettingService.GetAllAsync().Result.FirstOrDefault();
            if (churchSetting != null)
            {
                Logopath = churchSetting.Logo;
                ChurchName = churchSetting.Name;
            }
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
                var user = await _authService.GetVerifiedUserAsync(LoginData.UsernameOrEmail, LoginData.Password);
                if (user != null)
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserName", user.FullName);
                    HttpContext.Session.SetString("RoleId", user.RoleId);          // 👈 Add this
                    HttpContext.Session.SetString("RoleName", "Admin");    // 👈 Optional: friendly display name
                    var settings = await _churchSettingService.GetAllAsync();
                    if (settings.Any())
                    {
                        HttpContext.Session.SetString("ChurchName", settings.FirstOrDefault().Name);
                        HttpContext.Session.SetString("ChurchLogo", settings.FirstOrDefault().Logo);
                        return RedirectToPage("/ManagementSystem/Dashboard");
                    }
                    else
                    {
                        return Redirect("/setting");
                    }
                   

                }
            }

            ErrorMessage = "Invalid login credentials. Please try again.";
            return Page();

        }

        public async Task<IActionResult> OnPostQrLoginAsync([FromBody] string email)
        {
            if (string.IsNullOrEmpty(email))
                return new JsonResult(new { success = false, message = "No email provided." });

          
            var vendoruser = _context.Members.Where(x => x.Email == email).FirstOrDefault();
            var visitoruser = _context.Visitors.Where(x => x.Email == email).FirstOrDefault();

            if (vendoruser != null)
            {
                var rolid = _context.Roles.Where(x => x.Name == "Member").Select(x=>x.Id).FirstOrDefault();
                HttpContext.Session.SetString("UserEmail", vendoruser.Email);
                HttpContext.Session.SetString("UserName", vendoruser.FName);
                HttpContext.Session.SetString("RoleId", rolid.ToString());
                HttpContext.Session.SetString("RoleName", "Member");

                // Return redirect URL in the JSON response
                return new JsonResult(new { success = true, redirectUrl = "/memberdonations", userName= vendoruser.Email });
            }

            return new JsonResult(new { success = false, message = "User not found." });
        }



    }
}
