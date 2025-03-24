using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class DonationTypesModel : PageModel
    {
        private readonly IDonationTypeService _donationTypeService;

        public DonationTypesModel(IDonationTypeService donationTypeService)
        {
            _donationTypeService = donationTypeService;
        }

        // List to display in the table
        public List<DonationType> DonationTypeList { get; set; }

        // Bind DonationType for add/edit
        [BindProperty]
        public DonationType DonationType { get; set; } = new DonationType();

        // For delete handler
        [BindProperty]
        public Guid DonationTypeId { get; set; }


        public async Task OnGetAsync()
        {
            var types = await _donationTypeService.GetAllDonationTypesAsync();
            DonationTypeList = types.ToList();
            DonationType.Id = Guid.NewGuid();
        }

        // ✅ This matches: asp-page-handler="AddDonationType"
        public async Task<IActionResult> OnPostAddDonationTypeAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Please fill in all required fields.";
                await OnGetAsync();
                return Page();
            }

            DonationType.Id = Guid.NewGuid();
            await _donationTypeService.AddDonationTypeAsync(DonationType);
            return RedirectToPage();
        }

        // ✅ This matches: asp-page-handler="EditDonationType"
        public async Task<IActionResult> OnPostEditDonationTypeAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Invalid update attempt.";
                await OnGetAsync();
                return Page();
            }

            await _donationTypeService.UpdateDonationTypeAsync(DonationType);
            return RedirectToPage();
        }

        // ✅ This matches: asp-page-handler="DeleteDonationType"
        public async Task<IActionResult> OnPostDeleteDonationTypeAsync()
        {
            if (DonationTypeId == Guid.Empty)
            {
                TempData["FormError"] = "Invalid donation type ID.";
                await OnGetAsync();
                return Page();
            }

            await _donationTypeService.DeleteDonationTypeAsync(DonationTypeId);
            return RedirectToPage();
        }

    }
}
