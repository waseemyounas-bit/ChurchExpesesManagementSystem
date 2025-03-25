using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class BanksPageModel : PageModel
    {
        private readonly IBankAccountService _bankService;

        public BanksPageModel(IBankAccountService bankService)
        {
            _bankService = bankService;
        }

        [BindProperty]
        public BankAccount BankAccount { get; set; } = new BankAccount();

        public List<BankAccount> BankList { get; set; }

        // For delete handler
        [BindProperty]
        public Guid BankaccountId { get; set; }
        public async Task OnGetAsync()
        {
            BankList = (await _bankService.GetAllAsync())?.ToList();
            BankAccount.Id = new Guid();
        }

        public async Task<IActionResult> OnPostAddBankAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Repopulate list on error
                return Page();
            }

            await _bankService.CreateAsync(BankAccount);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteBankAsync(Guid bankId)
        {
            await _bankService.DeleteAsync(bankId);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditBankAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Repopulate list on error
                return Page();
            }

            await _bankService.UpdateAsync(BankAccount.Id, BankAccount);
            return RedirectToPage();
        }
    }
}
