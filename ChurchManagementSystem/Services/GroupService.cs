using DataAccess.Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GroupService : IGroupService
    {
        private readonly DataContext _context;

        public GroupService(DataContext context)
        {
            _context = context;
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            group.Id = Guid.NewGuid();
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateGroupAsync(Group group)
        {
            var existing = await _context.Groups.FindAsync(group.Id);
            if (existing == null)
                throw new Exception("Group not found.");

            _context.Entry(existing).CurrentValues.SetValues(group);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteGroupAsync(Guid id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
                return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Group> GetGroupByIdAsync(Guid id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }
    }

    public interface IGroupService
    {
        Task<Group> AddGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Group group);
        Task<bool> DeleteGroupAsync(Guid id);
        Task<Group> GetGroupByIdAsync(Guid id);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
    }
}
