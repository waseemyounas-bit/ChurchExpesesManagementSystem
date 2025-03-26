using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class RolePageModel : PageModel
    {
        private readonly IRoleService _roleService;

        public RolePageModel(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public List<IdentityRole> RoleList { get; set; } = new();

        [BindProperty]
        public IdentityRole InputRole { get; set; } = new IdentityRole();

        [BindProperty]
        public string RoleId { get; set; }

        public async Task OnGetAsync()
        {
            RoleList = _roleService.GetAllRoles().ToList();
            InputRole.Id = Guid.NewGuid().ToString();
        }

        public async Task<IActionResult> OnPostAddRoleAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(InputRole.Name))
            {
                await OnGetAsync();
                return Page();
            }

            var result = await _roleService.AddRoleAsync(InputRole.Name);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add role.");
                await OnGetAsync();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditRoleAsync()
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(InputRole.Id))
            {
                await OnGetAsync();
                return Page();
            }

            var result = await _roleService.UpdateRoleAsync(InputRole.Id, InputRole.Name);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to update role.");
                await OnGetAsync();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteRoleAsync()
        {
            if (string.IsNullOrEmpty(RoleId))
            {
                await OnGetAsync();
                return Page();
            }

            var result = await _roleService.DeleteRoleAsync(RoleId);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to delete role.");
                await OnGetAsync();
                return Page();
            }

            return RedirectToPage();
        }
    }
}
