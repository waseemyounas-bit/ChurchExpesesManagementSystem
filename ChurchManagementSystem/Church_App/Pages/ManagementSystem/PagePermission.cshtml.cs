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

        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
        //public SelectList Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            SelectedRoleId = Roles.FirstOrDefault()?.Id;
            if (!string.IsNullOrEmpty(SelectedRoleId))
            {
                Permissions = await _pagePermissionService.GetPermissionsByRoleAsync(SelectedRoleId);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {

            Roles = await _roleManager.Roles.ToListAsync();
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

            Roles = await _roleManager.Roles.ToListAsync(); // Add this
            Permissions = await _pagePermissionService.GetPermissionsByRoleAsync(SelectedRoleId); // Optional: reload view

            return Page(); // or RedirectToPage() if you want fresh state
        }


        public async Task<IActionResult> OnPostChangeRoleAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();

            if (!string.IsNullOrEmpty(SelectedRoleId))
            {
                Permissions = await _pagePermissionService.GetPermissionsByRoleAsync(SelectedRoleId);
            }

            return Page();
        }

    }
}
