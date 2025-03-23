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
    public class VisitorService: IVisitorService
    {
        private readonly DataContext _context;

        public VisitorService(DataContext context)
        {
            _context = context;
        }

        public async Task<Visitor> AddVisitorAsync(Visitor visitor)
        {
            visitor.Id = Guid.NewGuid();
            await _context.Visitors.AddAsync(visitor);
            await _context.SaveChangesAsync();
            return visitor;
        }

        public async Task<Visitor> UpdateVisitorAsync(Visitor visitor)
        {
            var existing = await _context.Visitors.FindAsync(visitor.Id);
            if (existing == null)
                throw new Exception("Visitor not found.");
            if (visitor.ImagePath==null)
            {
                visitor.ImagePath = existing.ImagePath;
            }
            _context.Entry(existing).CurrentValues.SetValues(visitor);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteVisitorAsync(Guid id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
                return false;

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Visitor> GetVisitorByIdAsync(Guid id)
        {
            return await _context.Visitors.FindAsync(id);
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitorsAsync()
        {
            return await _context.Visitors.ToListAsync();
        }
    }

    public interface IVisitorService
    {
        Task<Visitor> AddVisitorAsync(Visitor visitor);
        Task<Visitor> UpdateVisitorAsync(Visitor visitor);
        Task<bool> DeleteVisitorAsync(Guid id);
        Task<Visitor> GetVisitorByIdAsync(Guid id);
        Task<IEnumerable<Visitor>> GetAllVisitorsAsync();
    }
}
