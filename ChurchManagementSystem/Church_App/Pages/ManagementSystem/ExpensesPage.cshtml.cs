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

        public ExpensesPageModel(IExpenseService expenseService, IExpenseTypeService expenseTypeService)
        {
            _expenseService = expenseService;
            _expenseTypeService = expenseTypeService;
        }

        public List<Expense> ExpenseList { get; set; } = new();
        public SelectList ExpenseTypeOptions { get; set; }

        [BindProperty]
        public Expense Expense { get; set; } = new Expense();

        [BindProperty]
        public Guid ExpenseId { get; set; }

        public async Task OnGetAsync()
        {
            Expense.Date = DateTime.Now;
            ExpenseList = await _expenseService.GetAllAsync();
            var expenseTypes = await _expenseTypeService.GetAllAsync();
            ExpenseTypeOptions = new SelectList(expenseTypes, "Name", "Name");
        }

        public async Task<IActionResult> OnPostAddExpenseAsync()
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
