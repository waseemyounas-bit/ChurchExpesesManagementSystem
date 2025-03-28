using Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail)
                       ?? await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                return false;

            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result.Succeeded;
        }

        public async Task<IdentityResult> RegisterAsync(string email, string password,Member member, string RolId)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                 MemberId= member.Id, FirstName= member.FName,
                FullName = member.FName + " " + member.LName, RoleId = RolId
            };

            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public List<ApplicationUser> GetUsersWitMemberAsync()
        {
            return _userManager.Users
                    .Where(u => u.MemberId != null)
                    .ToList();
        }

    }
}
