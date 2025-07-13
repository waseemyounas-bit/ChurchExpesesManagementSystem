using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class VendorPaidExpansesModel : PageModel
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IVendorService _vendorService;

        public VendorPaidExpansesModel(IExpenseService expenseService, IExpenseTypeService expenseTypeService, IVendorService vendorService)
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
        public ExpenseViewModel Expense { get; set; } 

        [BindProperty]
        public Guid deleteExpenseId { get; set; }

        public async Task OnGetAsync()
        {
            ExpenseList = (await _expenseService.GetAllAsync()).Where(e => e.VendorId != null).ToList();
            var expenseTypes = await _expenseTypeService.GetAllAsync();
            ExpenseTypeOptions = new SelectList(expenseTypes, "Name", "Name");
            var vendors = await _vendorService.GetAllVendorsAsync();
            Vendorlist = vendors;
            VendorsOptions = new SelectList(vendors, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAddVendorPaidExpenseAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }
            Expense.Category="Vendor Paid";
            var expense = new Expense
            {
                Id = Guid.NewGuid(),
                Category = Expense.Category,
                Amount = Expense.Amount,
                Date = Expense.Date,
                Description = Expense.Description,
                VendorId = Expense.VendorId
            };

            await _expenseService.AddAsync(expense);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditExpenseAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }
            var expense = new Expense
            {
                Id = Expense.Id,
                Category = Expense.Category,
                Amount = Expense.Amount,
                Date = Expense.Date,
                Description = Expense.Description,
                VendorId = Expense.VendorId
            };
            await _expenseService.UpdateAsync(expense);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteExpenseAsync()
        {
            if (deleteExpenseId != Guid.Empty)
            {
                await _expenseService.DeleteAsync(deleteExpenseId);
            }

            return RedirectToPage();
        }
    }
}
