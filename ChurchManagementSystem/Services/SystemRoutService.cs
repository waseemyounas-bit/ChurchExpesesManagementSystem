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
    public class SystemRoutService : ISystemRoutService
    {
        private readonly DataContext _context;

        public SystemRoutService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SystemRout>> GetAllAsync()
        {
            return await _context.SystemRouts.ToListAsync();
        }
    }

    public interface ISystemRoutService
    {
        Task<IEnumerable<SystemRout>> GetAllAsync();
    }
}
