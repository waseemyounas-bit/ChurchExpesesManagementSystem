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
    public class BankAccountService: IBankAccountService
    {
        private readonly DataContext _context;

        public BankAccountService(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BankAccount>> GetAllAsync()
        {
            return await _context.BankAccounts.ToListAsync();
        }

        public async Task<BankAccount> GetByIdAsync(Guid id)
        {
            return await _context.BankAccounts.FindAsync(id);
        }

        public async Task<BankAccount> CreateAsync(BankAccount account)
        {
            account.Id = Guid.NewGuid();
            _context.BankAccounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<BankAccount> UpdateAsync(Guid id, BankAccount updatedAccount)
        {
            var existing = await _context.BankAccounts.FindAsync(id);
            if (existing == null) return null;

            existing.AccountNumber = updatedAccount.AccountNumber;
            existing.RoutingNumber = updatedAccount.RoutingNumber;
            existing.BankName = updatedAccount.BankName;
            existing.Address = updatedAccount.Address;
            existing.City = updatedAccount.City;
            existing.State = updatedAccount.State;
            existing.Zip = updatedAccount.Zip;
            existing.Phone = updatedAccount.Phone;
            existing.Contact = updatedAccount.Contact;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var account = await _context.BankAccounts.FindAsync(id);
            if (account == null) return false;

            _context.BankAccounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IBankAccountService
    {
        Task<IEnumerable<BankAccount>> GetAllAsync();
        Task<BankAccount> GetByIdAsync(Guid id);
        Task<BankAccount> CreateAsync(BankAccount account);
        Task<BankAccount> UpdateAsync(Guid id, BankAccount account);
        Task<bool> DeleteAsync(Guid id);
    }
}
