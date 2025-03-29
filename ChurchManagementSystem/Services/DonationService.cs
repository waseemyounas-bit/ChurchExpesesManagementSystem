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
    public class DonationService: IDonationService
    {
        private readonly DataContext _context;

        public DonationService(DataContext context)
        {
            _context = context;
        }
        // Get all donations
        public async Task<IEnumerable<Donation>> GetAllDonationsAsync()
        {
            return await _context.Donations
                                 .Include(d => d.Member).Include(d=>d.Visitor)  // Include Member for the join
                                 .ToListAsync();
        }
        // Get all donations
        public List<Donation> GetAllDonations()
        {
            return  _context.Donations
                                 .Include(d => d.Member).Include(d => d.Visitor)  // Include Member for the join
                                 .ToList();
        }
        public List<Donation> GetAllDonations(DateTime fromdate, DateTime todate)
        {
            return _context.Donations.Where(x=>x.Date.Date>= fromdate.Date && x.Date <= todate.Date)
                                 .Include(d => d.Member).Include(d => d.Visitor)  // Include Member for the join
                                 .ToList();
        }

        public List<Donation> GetAllDonations(DateTime fromdate, DateTime todate, string type)
        {
            if (type=="M")
            {
                return _context.Donations.Where(x => x.Date.Date >= fromdate.Date && x.Date <= todate.Date && x.VisitorId==null)
                                     .Include(d => d.Member).Include(d => d.Visitor)  // Include Member for the join
                                     .ToList();
            }
            else
            {
                return _context.Donations.Where(x => x.Date.Date >= fromdate.Date && x.Date <= todate.Date && x.MemberId==null)
                                     .Include(d => d.Member).Include(d => d.Visitor)  // Include Member for the join
                                     .ToList();
            }
                
        }
        // Get a donation by its ID
        public async Task<Donation> GetDonationByIdAsync(Guid donationId)
        {
            return await _context.Donations
                                 .Include(d => d.Member)
                                 .FirstOrDefaultAsync(d => d.Id == donationId);
        }

        // Add a new donation
        public async Task AddDonationAsync(Donation donation)
        {
            await _context.Donations.AddAsync(donation);
            await _context.SaveChangesAsync();
        }

        // Update an existing donation
        public async Task UpdateDonationAsync(Donation donation)
        {
            _context.Donations.Update(donation);
            await _context.SaveChangesAsync();
        }

        // Delete a donation
        public async Task DeleteDonationAsync(Guid donationId)
        {
            var donation = await _context.Donations.FindAsync(donationId);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
                await _context.SaveChangesAsync();
            }
        }
    }

    public interface IDonationService
    {
        Task<IEnumerable<Donation>> GetAllDonationsAsync();
        Task<Donation> GetDonationByIdAsync(Guid donationId);
        Task AddDonationAsync(Donation donation);
        Task UpdateDonationAsync(Donation donation);
        Task DeleteDonationAsync(Guid donationId);
        List<Donation> GetAllDonations();
        List<Donation> GetAllDonations(DateTime fromdate, DateTime todate);
        List<Donation> GetAllDonations(DateTime fromdate, DateTime todate, string type);
    }
}
