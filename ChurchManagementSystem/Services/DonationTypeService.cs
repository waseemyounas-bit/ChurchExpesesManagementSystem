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
    public class DonationTypeService: IDonationTypeService
    {
        private readonly DataContext _context;

        public DonationTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<DonationType> AddDonationTypeAsync(DonationType donationType)
        {
            donationType.Id = Guid.NewGuid();
            await _context.DonationTypes.AddAsync(donationType);
            await _context.SaveChangesAsync();
            return donationType;
        }

        public async Task<DonationType> UpdateDonationTypeAsync(DonationType donationType)
        {
            var existing = await _context.DonationTypes.FindAsync(donationType.Id);
            if (existing == null)
                throw new Exception("Donation type not found.");

            _context.Entry(existing).CurrentValues.SetValues(donationType);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteDonationTypeAsync(Guid id)
        {
            var type = await _context.DonationTypes.FindAsync(id);
            if (type == null)
                return false;

            _context.DonationTypes.Remove(type);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DonationType> GetDonationTypeByIdAsync(Guid id)
        {
            return await _context.DonationTypes.FindAsync(id);
        }

        public async Task<IEnumerable<DonationType>> GetAllDonationTypesAsync()
        {
            return await _context.DonationTypes.ToListAsync();
        }
    }

    public interface IDonationTypeService
    {
        Task<DonationType> AddDonationTypeAsync(DonationType donationType);
        Task<DonationType> UpdateDonationTypeAsync(DonationType donationType);
        Task<bool> DeleteDonationTypeAsync(Guid id);
        Task<DonationType> GetDonationTypeByIdAsync(Guid id);
        Task<IEnumerable<DonationType>> GetAllDonationTypesAsync();
    }
}
