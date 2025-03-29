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

        public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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

        public async Task<ApplicationUser?> GetVerifiedUserAsync(string usernameOrEmail, string password)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail)
                       ?? await _userManager.FindByEmailAsync(usernameOrEmail);

            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
                return null;

            return user;
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

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser updatedUser, string? newPassword = null)
        {
            var user = await _userManager.FindByIdAsync(updatedUser.Id);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            // Update basic fields
            user.FirstName = updatedUser.FirstName;
            user.FullName = updatedUser.FullName;
            user.PhoneNumber = updatedUser.PhoneNumber;

            // Update password if provided
            IdentityResult passwordResult = IdentityResult.Success;
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                passwordResult = await _userManager.ResetPasswordAsync(user, token, newPassword);
                if (!passwordResult.Succeeded)
                    return passwordResult;
            }

            // Save updates
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }



    }
}
