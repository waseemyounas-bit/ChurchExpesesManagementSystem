using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class DashboardModel : PageModel
    {
        private readonly IExpenseService _expenseService;
        private readonly IDonationService _donationService;
        private readonly IDepositService _depositService;
        private readonly IMemberService _memberService;
        private readonly IVisitorService _visitorservie;
        public DashboardModel(IExpenseService expenseService,IDonationService donationService, IDepositService depositService, IMemberService memberService, IVisitorService visitorService)
        {
            this._expenseService = expenseService;
            this._donationService = donationService;
            this._depositService = depositService;
            this._memberService = memberService;
            this._visitorservie = visitorService;
        }
        public double ThismonthDeposit { get; set; }
        public double ThismonthExpense { get; set; }
        public double ThismonthDonation { get; set; }
        public int Members { get; set; }
        public int Visitors { get; set; }
        public List<Donation> DonationList { get; set; }

        public void OnGet()
        {
            ThismonthDeposit = _depositService.GetAll(DateTime.Now.AddDays(-30), DateTime.Now).Sum(x => x.Total);
            ThismonthExpense =Convert.ToDouble(_expenseService.GetExpenses(DateTime.Now.AddDays(-30), DateTime.Now).Sum(x => x.Amount));
            ThismonthDonation =Convert.ToDouble(_donationService.GetAllDonations(DateTime.Now.AddDays(-30), DateTime.Now).Sum(x => x.Amount));
            Members = _memberService.GetAllMembers().Count;
            Visitors= _visitorservie.GetAllVisitors().Count();
            DonationList = _donationService.GetAllDonations(DateTime.Now.AddDays(-30), DateTime.Now);
           
        }
    }
}
