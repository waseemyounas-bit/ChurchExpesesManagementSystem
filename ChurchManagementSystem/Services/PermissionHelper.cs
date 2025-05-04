using DataAccess.Data;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PermissionHelper
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHelper(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> HasAccessAsync(string routeName)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated) return false;

            var roleName = httpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(roleName)) return false;

            var route = await _context.SystemRouts.FirstOrDefaultAsync(r => r.Name.ToLower() == routeName.ToLower());
            if (route == null) return false;

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null) return false;

            var permission = await _context.Set<RoutPermission>()
                .FirstOrDefaultAsync(p => p.RolId == role.Id && p.RoutId == route.Id && p.IsAllow);

            return permission != null && permission.IsAllow;
        }

        public async Task<bool> HasAccessAsync(string routeName, string roleId)
        {
            if (string.IsNullOrEmpty(routeName) || string.IsNullOrEmpty(roleId))
                return false;

            // Check if role is Admin
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (role != null && (role.Name == "Admin" || role.Name== "Member"))
            {
                return true; // Admins have access to all routes
            }

            // Get route info
            var route = await _context.SystemRouts
                .FirstOrDefaultAsync(r => r.Name.ToLower() == routeName.ToLower());

            if (route == null)
                return false;

            // Check permission
            var permission = await _context.Set<RoutPermission>()
                .FirstOrDefaultAsync(p => p.RolId == roleId && p.RoutId == route.Id && p.IsAllow);

            return permission != null && permission.IsAllow;
        }

    }
}
