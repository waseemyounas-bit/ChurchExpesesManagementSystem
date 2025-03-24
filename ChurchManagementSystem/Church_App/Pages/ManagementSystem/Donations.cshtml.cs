using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{

    public class DonationsModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly IDonationTypeService _donationTypeService;
        private readonly IDonationService _donationService;

        [BindProperty]
        public Donation Donation { get; set; }

        // List to display in the table
        public List<Donation> DonationList { get; set; }
        public List<Member> Donors { get; set; } = new List<Member>();
        public List<DonationType> DonationTypes { get; set; } = new List<DonationType>();

        // For delete handler
        [BindProperty]
        public Guid DonationId { get; set; }
        public DonationsModel(IDonationTypeService donationTypeService, IMemberService memberService, IDonationService donationService)
        {
            this._donationTypeService = donationTypeService;
            this._memberService = memberService;
            _donationService = donationService;
        }
        public void OnGet()
        {
            Donors = _memberService.GetAllMembers();
            DonationTypes = _donationTypeService.GetAllDonationType();
            var donations =  _donationService.GetAllDonations();
            DonationList = donations.ToList();
        }

    

        // ✅ This matches: asp-page-handler="AddDonation"
        public async Task<IActionResult> OnPostAddDonationAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Please fill in all required fields.";
                 OnGet();
                return Page();
            }

            Donation.Id = Guid.NewGuid();
            await _donationService.AddDonationAsync(Donation);
            return RedirectToPage();
        }

        // ✅ This matches: asp-page-handler="EditDonation"
        public async Task<IActionResult> OnPostEditDonationAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Invalid update attempt.";
                OnGet();
                return Page();
            }

            await _donationService.UpdateDonationAsync(Donation);
            return RedirectToPage();
        }

        // ✅ This matches: asp-page-handler="DeleteDonation"
        public async Task<IActionResult> OnPostDeleteDonationAsync()
        {
            if (DonationId == Guid.Empty)
            {
                TempData["FormError"] = "Invalid donation ID.";
                OnGet();
                return Page();
            }

            await _donationService.DeleteDonationAsync(DonationId);
            return RedirectToPage();
        }
    }
}

