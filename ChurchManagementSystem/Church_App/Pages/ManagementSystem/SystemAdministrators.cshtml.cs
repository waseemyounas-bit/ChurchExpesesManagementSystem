using Entities;
using Entities.DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services;

namespace Church_App.Pages.ManagementSystem
{
    public class SystemAdministratorsModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly IRoleService _roleService;
        private readonly AuthService _authService;
        public SystemAdministratorsModel(IMemberService memberService, IRoleService roleService, AuthService authService)
        {
            this._memberService = memberService;
            this._roleService = roleService;
            _authService = authService;
        }
        [BindProperty]
        public AccessManageDTO Acccess { get; set; }

        public List<Member> members { get; set; } = new List<Member>();
        public List<IdentityRole> roles { get; set; } = new List<IdentityRole>();
        public List<ApplicationUser> applicationUsers { get; set; } = new List<ApplicationUser>();
        public void OnGet()
        {
            members = _memberService.GetAllMembers().ToList();
            roles = _roleService.GetAllRoles().ToList();
            applicationUsers=_authService.GetUsersWitMemberAsync();

        }

        public async Task<IActionResult> OnPostAddAdminAsync()
        {
            if (!ModelState.IsValid)
            {
                 OnGet();
                return Page();
            }
            Member member =await _memberService.GetMemberByIdAsync(Acccess.MemberId);
            var result =await _authService.RegisterAsync(Acccess.Email,Acccess.Password, member,Acccess.RolId);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add role.");
                OnGet();
                return Page();
            }

            return RedirectToPage();
        }
    }
}
