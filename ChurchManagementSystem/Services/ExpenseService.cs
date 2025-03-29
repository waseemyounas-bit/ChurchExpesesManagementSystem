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
    public class ExpenseService: IExpenseService
    {
        private readonly DataContext _context;

        public ExpenseService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _context.Expenses.ToListAsync();
        }

        public List<Expense> GetExpenses(DateTime fromdate, DateTime todate)
        {
            return  _context.Expenses.Where(x=>x.Date >= fromdate.Date && x.Date <= todate.Date).ToList();
        }
        public List<Expense> GetExpenses(DateTime fromdate, DateTime todate, string type)
        {
            return _context.Expenses.Where(x => x.Date >= fromdate.Date && x.Date <= todate.Date && x.Category==type).ToList();
        }
        public async Task<Expense> GetByIdAsync(Guid id)
        {
            return await _context.Expenses.FindAsync(id);
        }

        public async Task AddAsync(Expense expense)
        {
            expense.Id = Guid.NewGuid();
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            var existing = await _context.Expenses.FindAsync(expense.Id);
            if (existing != null)
            {
                existing.Category = expense.Category;
                existing.Amount = expense.Amount;
                existing.Date = expense.Date;
                existing.Description = expense.Description;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IExpenseService
    {
        Task<List<Expense>> GetAllAsync();
        List<Expense> GetExpenses(DateTime fromdate, DateTime todate);
        List<Expense> GetExpenses(DateTime fromdate, DateTime todate, string type);
        Task<Expense> GetByIdAsync(Guid id);
        Task AddAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Guid id);
    }
}
