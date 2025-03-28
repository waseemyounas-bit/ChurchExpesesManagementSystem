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
    public class VendorService: IVendorService
    {
        private readonly DataContext _context;

        public VendorService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            return await _context.Vendors.ToListAsync();
        }
        public List<Vendor> GetAllVendors()
        {
            return  _context.Vendors.ToList();
        }
        public async Task<Vendor> GetVendorByIdAsync(Guid id)
        {
            return await _context.Vendors.FindAsync(id);
        }

        public async Task<Vendor> AddVendorAsync(Vendor vendor)
        {
            vendor.Id = Guid.NewGuid();
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();
            return vendor;
        }

        public async Task<bool> UpdateVendorAsync(Vendor vendor)
        {
            var existingVendor = await _context.Vendors.FindAsync(vendor.Id);
            if (existingVendor == null)
                return false;

            existingVendor.Name = vendor.Name;
            existingVendor.Contact = vendor.Contact;
            existingVendor.Address = vendor.Address;
            existingVendor.AccountNumber = vendor.AccountNumber;
            existingVendor.Email = vendor.Email;
            existingVendor.City = vendor.City;
            existingVendor.State = vendor.State;
            existingVendor.Zip = vendor.Zip;

            _context.Vendors.Update(existingVendor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVendorAsync(Guid id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
                return false;

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IVendorService
    {
        Task<List<Vendor>> GetAllVendorsAsync();
        Task<Vendor> GetVendorByIdAsync(Guid id);
        Task<Vendor> AddVendorAsync(Vendor vendor);
        Task<bool> UpdateVendorAsync(Vendor vendor);
        Task<bool> DeleteVendorAsync(Guid id);
        List<Vendor> GetAllVendors();
    }
}
