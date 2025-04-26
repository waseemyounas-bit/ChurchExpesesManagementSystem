using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class ExpensesPageModel : PageModel
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IVendorService _vendorService;

        public ExpensesPageModel(IExpenseService expenseService, IExpenseTypeService expenseTypeService, IVendorService vendorService)
        {
            _expenseService = expenseService;
            _expenseTypeService = expenseTypeService;
            _vendorService = vendorService;
        }

        public List<Expense> ExpenseList { get; set; } = new();

        public SelectList ExpenseTypeOptions { get; set; }
        public SelectList VendorsOptions { get; set; }

        public List<Vendor> Vendorlist { get; set; }
        [BindProperty]
        public Expense Expense { get; set; } = new Expense();

        [BindProperty]
        public Guid ExpenseId { get; set; }

        public bool IsToPayVendor { get; set; } // you'll need to bind this manually from form
        public async Task OnGetAsync()
        {
            ExpenseList = (await _expenseService.GetAllAsync()).Where(e => e.VendorId == null).ToList();
            var expenseTypes = await _expenseTypeService.GetAllAsync();
            ExpenseTypeOptions = new SelectList(expenseTypes, "Name", "Name");
        }

        public async Task<IActionResult> OnPostAddNonVendorPaidExpenseAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            Expense.Id = Guid.NewGuid();
            await _expenseService.AddAsync(Expense);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditExpenseAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            await _expenseService.UpdateAsync(Expense);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteExpenseAsync()
        {
            if (ExpenseId != Guid.Empty)
            {
                await _expenseService.DeleteAsync(ExpenseId);
            }

            return RedirectToPage();
        }
    }
}
