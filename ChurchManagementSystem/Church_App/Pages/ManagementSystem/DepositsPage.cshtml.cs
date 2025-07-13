using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class DepositsPageModel : PageModel
    {
        private readonly IVendorService _vendorService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IDepositService _depositService;
        public DepositsPageModel(IVendorService vendorService, IBankAccountService bankAccountService, IDepositService depositService)
        {
            _vendorService = vendorService;
            _bankAccountService = bankAccountService;
            _depositService = depositService;
        }

        [BindProperty]
        public DepositTB Deposit { get; set; }

        [BindProperty]
        public List<string> Checks { get; set; }

        public List<string> DistinctBanks { get; set; } = new();
        public List<BankAccount> Banks { get; set; } = new List<BankAccount>();
        public List<DepositTB> Deposits { get; set; }
        public void OnGet()
        {
            Banks = _bankAccountService.GetAllBank();
            DistinctBanks = Banks
                .Select(b => b.BankName)
                .Distinct()
                .OrderBy(b => b)
                .ToList();
            Deposits = _depositService.GetAll();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                 OnGet();
                return Page();
            }
            
            if (Checks.Count()>0)
            {
                foreach (var item in Checks)
                {
                    Deposit.Check += "," + item;
                }
            }
          
          
            await _depositService.CreateAsync(Deposit);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            await _depositService.UpdateAsync(Deposit.Id, Deposit);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _depositService.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
