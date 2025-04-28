using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages
{
    public class ChurchSettingUpdateModel : PageModel
    {
        private readonly IChurchSettingService _churchSettingService;
        private readonly IWebHostEnvironment _environment;

        public ChurchSettingUpdateModel(IChurchSettingService churchSettingService, IWebHostEnvironment environment)
        {
            _churchSettingService = churchSettingService;
            _environment = environment;
        }

        [BindProperty]
        public ChurchSetting ChurchSetting { get; set; } = new();

        [BindProperty]
        public IFormFile? LogoFile { get; set; }
        public string ErrorMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve the current ChurchSetting from the database, possibly based on a session or database ID
            var churchName = HttpContext.Session.GetString("ChurchName");
            if (churchName != null)
            {
                ChurchSetting = (await _churchSettingService.GetAllAsync()).Where(x=>x.Name== churchName).FirstOrDefault();
                if (ChurchSetting == null)
                {
                    ErrorMessage = "Church setting not found.";
                    return Page();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Update the ChurchSetting object
            await _churchSettingService.UpdateAsync(ChurchSetting.Id,ChurchSetting);
            HttpContext.Session.SetString("ChurchName", ChurchSetting.Name);
            HttpContext.Session.SetString("ChurchLogo", ChurchSetting.Logo);

            return Redirect("/dashboard"); // Redirect after update
        }
    }
}
