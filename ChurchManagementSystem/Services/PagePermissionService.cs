using DataAccess.Data;
using Entities;
using Entities.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PagePermissionService: IPagePermissionService
    {
        private readonly DataContext _context;

        public PagePermissionService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PagePermissionDto>> GetPermissionsByRoleAsync(string roleId)
        {
            var allRoutes = await _context.SystemRouts.ToListAsync();
            var permissions = await _context.Set<RoutPermission>()
                .Where(r => r.RolId == roleId)
                .ToListAsync();

            return allRoutes.Select(route => new PagePermissionDto
            {
                RoutId = route.Id,
                PageName = route.Name,
                IsAllow = permissions.FirstOrDefault(p => p.RoutId == route.Id)?.IsAllow ?? false
            }).ToList();
        }

        public async Task UpdatePermissionsAsync(string roleId, List<PagePermissionDto> permissions)
        {
            var existingPermissions = await _context.Set<RoutPermission>()
                .Where(r => r.RolId == roleId)
                .ToListAsync();

            foreach (var item in permissions)
            {
                var permission = existingPermissions.FirstOrDefault(p => p.RoutId == item.RoutId);
                if (permission == null)
                {
                    _context.Add(new RoutPermission
                    {
                        Id = Guid.NewGuid(),
                        RolId = roleId,
                        RoutId = item.RoutId,
                        IsAllow = item.IsAllow
                    });
                }
                else
                {
                    permission.IsAllow = item.IsAllow;
                    _context.Update(permission);
                }
            }

            await _context.SaveChangesAsync();
        }
    }

    public interface IPagePermissionService
    {
        Task<List<PagePermissionDto>> GetPermissionsByRoleAsync(string roleId);
        Task UpdatePermissionsAsync(string roleId, List<PagePermissionDto> permissions);
    }
}
