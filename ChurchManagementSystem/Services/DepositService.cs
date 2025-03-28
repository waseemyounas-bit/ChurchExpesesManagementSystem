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
    public class DepositService: IDepositService
    {
        private readonly DataContext _context;

        public DepositService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepositTB>> GetAllAsync()
        {
            return await _context.DepositTBs.OrderByDescending(d => d.Date).ToListAsync();
        }
        public List<DepositTB> GetAll()
        {
            return  _context.DepositTBs.OrderByDescending(d => d.Date).ToList();
        }

        public async Task<DepositTB> GetByIdAsync(Guid id)
        {
            return await _context.DepositTBs.FindAsync(id);
        }

        public async Task<DepositTB> CreateAsync(DepositTB deposit)
        {
            deposit.Id = Guid.NewGuid();
            deposit.Total = deposit.Cash + deposit.Cheque;
            _context.DepositTBs.Add(deposit);
            await _context.SaveChangesAsync();
            return deposit;
        }

        public async Task<DepositTB> UpdateAsync(Guid id, DepositTB updatedDeposit)
        {
            var existing = await _context.DepositTBs.FindAsync(id);
            if (existing == null) return null;

            existing.Bank = updatedDeposit.Bank;
            existing.AccountNumber = updatedDeposit.AccountNumber;
            existing.Cash = updatedDeposit.Cash;
            existing.Cheque = updatedDeposit.Cheque;
            existing.Date = updatedDeposit.Date;
            existing.Total = updatedDeposit.Cash + updatedDeposit.Cheque;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deposit = await _context.DepositTBs.FindAsync(id);
            if (deposit == null) return false;

            _context.DepositTBs.Remove(deposit);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IDepositService
    {
        Task<IEnumerable<DepositTB>> GetAllAsync();
        Task<DepositTB> GetByIdAsync(Guid id);
        Task<DepositTB> CreateAsync(DepositTB deposit);
        Task<DepositTB> UpdateAsync(Guid id, DepositTB deposit);
        Task<bool> DeleteAsync(Guid id);
        List<DepositTB> GetAll();
    }
}
