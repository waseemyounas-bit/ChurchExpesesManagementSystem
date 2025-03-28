using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class ExpenseTypePageModel : PageModel
    {
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypePageModel(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        public List<ExpenseType> ExpenseTypeList { get; set; }

        [BindProperty]
        public ExpenseType ExpenseType { get; set; } = new ExpenseType();

        [BindProperty]
        public Guid ExpenseTypeId { get; set; }

        public async Task OnGetAsync()
        {
            ExpenseTypeList = await _expenseTypeService.GetAllAsync();
            ExpenseType.Id = Guid.NewGuid();
        }

        public async Task<IActionResult> OnPostAddExpenseTypeAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Please provide a valid name.";
                await OnGetAsync();
                return Page();
            }

            ExpenseType.Id = Guid.NewGuid();
            await _expenseTypeService.AddAsync(ExpenseType);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditExpenseTypeAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["FormError"] = "Invalid update attempt.";
                await OnGetAsync();
                return Page();
            }

            await _expenseTypeService.UpdateAsync(ExpenseType);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteExpenseTypeAsync()
        {
            if (ExpenseTypeId == Guid.Empty)
            {
                TempData["FormError"] = "Invalid expense type ID.";
                await OnGetAsync();
                return Page();
            }

            await _expenseTypeService.DeleteAsync(ExpenseTypeId);
            return RedirectToPage();
        }
    }
}
