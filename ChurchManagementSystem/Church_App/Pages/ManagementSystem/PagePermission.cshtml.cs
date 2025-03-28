using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class PagePermissionModel : PageModel
    {
        private readonly IPagePermissionService _pagePermissionService;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PagePermissionModel(IPagePermissionService pagePermissionService, RoleManager<IdentityRole> roleManager)
        {
            _pagePermissionService = pagePermissionService;
            _roleManager = roleManager;
        }

        [BindProperty]
        public string SelectedRoleId { get; set; }

        [BindProperty]
        public List<PagePermissionDto> Permissions { get; set; }

        public SelectList Roles { get; set; }

        public async Task OnGetAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Roles = new SelectList(roles, "Id", "Name");

            if (!string.IsNullOrEmpty(SelectedRoleId))
            {
                Permissions = await _pagePermissionService.GetPermissionsByRoleAsync(SelectedRoleId);
            }
        }

        public async Task OnGetAsync(string roleId)
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Roles = new SelectList(roles, "Id", "Name");

            if (!string.IsNullOrEmpty(roleId))
            {
                SelectedRoleId = roleId;
                Permissions = await _pagePermissionService.GetPermissionsByRoleAsync(roleId);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            Roles = new SelectList(roles, "Id", "Name");

            if (!string.IsNullOrEmpty(SelectedRoleId))
            {
                Permissions = await _pagePermissionService.GetPermissionsByRoleAsync(SelectedRoleId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            await _pagePermissionService.UpdatePermissionsAsync(SelectedRoleId, Permissions);
            TempData["Message"] = "Permissions updated successfully!";
            return RedirectToPage();
        }
    }
}
