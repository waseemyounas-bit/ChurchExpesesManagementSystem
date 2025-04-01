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
    public class ChurchSettingService: IChurchSettingService
    {
        private readonly DataContext _context;

        public ChurchSettingService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChurchSetting>> GetAllAsync()
        {
            return await _context.ChurchSettings.ToListAsync();
        }

        public async Task<ChurchSetting?> GetByIdAsync(Guid id)
        {
            return await _context.ChurchSettings.FindAsync(id);
        }

        public async Task<ChurchSetting> CreateAsync(ChurchSetting setting)
        {
            setting.Id = Guid.NewGuid();
            _context.ChurchSettings.Add(setting);
            await _context.SaveChangesAsync();
            return setting;
        }

        public async Task<ChurchSetting?> UpdateAsync(Guid id, ChurchSetting updatedSetting)
        {
            var existing = await _context.ChurchSettings.FindAsync(id);
            if (existing == null) return null;

            existing.Name = updatedSetting.Name;
            existing.Address = updatedSetting.Address;
            existing.PhoneNumber = updatedSetting.PhoneNumber;
            existing.Logo = updatedSetting.Logo;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var setting = await _context.ChurchSettings.FindAsync(id);
            if (setting == null) return false;

            _context.ChurchSettings.Remove(setting);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public interface IChurchSettingService
    {
        Task<IEnumerable<ChurchSetting>> GetAllAsync();
        Task<ChurchSetting?> GetByIdAsync(Guid id);
        Task<ChurchSetting> CreateAsync(ChurchSetting setting);
        Task<ChurchSetting?> UpdateAsync(Guid id, ChurchSetting updatedSetting);
        Task<bool> DeleteAsync(Guid id);
    }
}
