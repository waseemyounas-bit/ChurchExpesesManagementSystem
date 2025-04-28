using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;
using Syncfusion.DocIO.DLS;

namespace Church_App.Pages
{
    public class ChurchSettingModel : PageModel
    {
        private readonly IChurchSettingService _churchSettingService;
        private readonly IWebHostEnvironment _environment;

        public ChurchSettingModel(IChurchSettingService churchSettingService, IWebHostEnvironment environment)
        {
            _churchSettingService = churchSettingService;
            _environment = environment;
        }

        [BindProperty]
        public ChurchSetting ChurchSetting { get; set; } = new();

        [BindProperty]
        public IFormFile? LogoFile { get; set; }
        public string ErrorMessage { get; set; } = "";


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

           
            await _churchSettingService.CreateAsync(ChurchSetting);
            HttpContext.Session.SetString("ChurchName", ChurchSetting.Name);
            HttpContext.Session.SetString("ChurchLogo", ChurchSetting.Logo);
            return Redirect("/dashboard"); // Redirect after save
        }
    }
}
