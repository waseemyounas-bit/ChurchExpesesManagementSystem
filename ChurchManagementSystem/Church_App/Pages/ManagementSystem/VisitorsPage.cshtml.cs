using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class VisitorsPageModel : PageModel
    {
        private readonly IVisitorService _visitorService;
        private readonly IWebHostEnvironment _env;
        public VisitorsPageModel(IVisitorService visitorService, IWebHostEnvironment env)
        {
            _visitorService = visitorService;
            _env = env;
        }
        public List<Visitor> VisitorsList { get; set; }

        [BindProperty]
        public Visitor Visitor { get; set; } = new Visitor();

        [BindProperty]
        public IFormFile? VisitorImage { get; set; }

        [BindProperty]
        public Guid VisitorId { get; set; }
        public async Task OnGetAsync()
        {
            var visitors = await _visitorService.GetAllVisitorsAsync();
            VisitorsList = new List<Visitor>(visitors);
            Visitor.Id = Guid.NewGuid();
            Visitor.VisitDate = DateTime.Now;
        }

        public async Task<IActionResult> OnPostAddVisitorAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = GetFormErrors();
                await OnGetAsync();
                return Page();
            }

            if (VisitorImage != null && VisitorImage.Length > 0)
            {
                Visitor.ImagePath = await SaveImageAsync(VisitorImage);
            }

            Visitor.Id = Guid.NewGuid();
            await _visitorService.AddVisitorAsync(Visitor);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditVisitorAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = GetFormErrors();
                await OnGetAsync();
                return Page();
            }

            if (VisitorImage != null && VisitorImage.Length > 0)
            {
                Visitor.ImagePath = await SaveImageAsync(VisitorImage);
            }

            await _visitorService.UpdateVisitorAsync(Visitor);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteVisitorAsync()
        {
            if (VisitorId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Invalid visitor ID.");
                await OnGetAsync();
                return Page();
            }

            await _visitorService.DeleteVisitorAsync(VisitorId);
            return RedirectToPage();
        }


        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + fileName;
        }


        private string GetFormErrors()
        {
            var invalidFields = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => x.Key)
                .ToList();
            return "Invalid fields: " + string.Join(", ", invalidFields);
        }
    }
}
