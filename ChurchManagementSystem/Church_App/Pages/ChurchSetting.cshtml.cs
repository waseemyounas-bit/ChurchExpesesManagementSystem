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


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Handle image upload
         
            if (LogoFile != null && LogoFile.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(LogoFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }

                ChurchSetting.Logo = "/uploads/" + fileName;
            }

            await _churchSettingService.CreateAsync(ChurchSetting);
            HttpContext.Session.SetString("ChurchName", ChurchSetting.Name);
            HttpContext.Session.SetString("ChurchLogo", ChurchSetting.Logo);
            return Redirect("/dashboard"); // Redirect after save
        }
    }
}
