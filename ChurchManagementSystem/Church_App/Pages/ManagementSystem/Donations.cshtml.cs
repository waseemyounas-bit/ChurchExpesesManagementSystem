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
        private readonly IVisitorService _visitorService;
        [BindProperty]
        public Donation Donation { get; set; }

        public string RoleName { get; set; }
        // List to display in the table
        public List<Donation> DonationList { get; set; }
        public List<Member> Donors { get; set; } = new List<Member>();
        public List<Visitor> Visitors { get; set; } = new List<Visitor>();
        public List<DonationType> DonationTypes { get; set; } = new List<DonationType>();

        // For delete handler
        [BindProperty]
        public Guid DonationId { get; set; }
        private  readonly IHttpContextAccessor _httpContextAccessor;
        public DonationsModel(IDonationTypeService donationTypeService, IMemberService memberService, IDonationService donationService, IVisitorService visitorService, IHttpContextAccessor httpContextAccessor)
        {
            this._donationTypeService = donationTypeService;
            this._memberService = memberService;
            _donationService = donationService;
            _visitorService = visitorService;
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
            RoleName = _httpContextAccessor.HttpContext.Session.GetString("RoleName");
            Visitors = _visitorService.GetAllVisitors();
            DonationTypes = _donationTypeService.GetAllDonationType();
            if (RoleName=="Admin")
            {
                Donors = _memberService.GetAllMembers();
                var donations = _donationService.GetAllDonations();
                DonationList = donations.Where(x => x.MemberId != null).ToList();
            }
            else
            {
                var membermail= _httpContextAccessor.HttpContext.Session.GetString("UserEmail");
                var member = _memberService.GetAllMembers().FirstOrDefault(x => x.Email == membermail);
                Donors = _memberService.GetAllMembers().Where(x=>x.Id==member.Id).ToList();
                var donations = _donationService.GetAllDonations();
                DonationList = donations.Where(x => x.MemberId != null && x.MemberId == member.Id).ToList();
            }

               
    
        }

    

        // ✅ This matches: asp-page-handler="AddDonation"
        public async Task<IActionResult> OnPostAddDonationAsync()
        {
            //var donor = Donation;
            //if (!ModelState.IsValid)
            //{
            //    TempData["FormError"] = "Please fill in all required fields.";
            //     OnGet();
            //    return Page();
            //}

            Donation.Id = Guid.NewGuid();
            await _donationService.AddDonationAsync(Donation);
            return RedirectToPage();
        }

        // ✅ This matches: asp-page-handler="EditDonation"
        public async Task<IActionResult> OnPostEditDonationAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    TempData["FormError"] = "Invalid update attempt.";
            //    OnGet();
            //    return Page();
            //}

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

