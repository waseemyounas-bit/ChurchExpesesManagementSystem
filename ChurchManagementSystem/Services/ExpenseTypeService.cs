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
    public class ExpenseTypeService: IExpenseTypeService
    {
        private readonly DataContext _context;

        public ExpenseTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ExpenseType>> GetAllAsync()
        {
            return await _context.ExpenseTypes.ToListAsync();
        }

        public async Task<ExpenseType?> GetByIdAsync(Guid id)
        {
            return await _context.ExpenseTypes.FindAsync(id);
        }

        public async Task AddAsync(ExpenseType expenseType)
        {
            expenseType.Id = Guid.NewGuid();
            _context.ExpenseTypes.Add(expenseType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(ExpenseType expenseType)
        {
            var existing = await _context.ExpenseTypes.FindAsync(expenseType.Id);
            if (existing == null) return false;

            existing.Name = expenseType.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var expenseType = await _context.ExpenseTypes.FindAsync(id);
            if (expenseType == null) return false;

            _context.ExpenseTypes.Remove(expenseType);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IExpenseTypeService
    {
        Task<List<ExpenseType>> GetAllAsync();
        Task<ExpenseType?> GetByIdAsync(Guid id);
        Task AddAsync(ExpenseType expenseType);
        Task<bool> UpdateAsync(ExpenseType expenseType);
        Task<bool> DeleteAsync(Guid id);
    }
}
