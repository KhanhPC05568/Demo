using Demo.Interface.Repositories;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Data;

namespace Demo.Repositories
{
    public class AllowAccessRepository : IAllowAccessRepository
    {
        private readonly ApplicationDbContext _context;

        public AllowAccessRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AllowAccess> GetByIdAsync(int id)
        {
            return await _context.AllowAccesses.FindAsync(id);
        }

        public async Task<IEnumerable<AllowAccess>> GetAllAsync()
        {
            return await _context.AllowAccesses.ToListAsync();
        }

        public async Task AddAsync(AllowAccess entity)
        {
            await _context.AllowAccesses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AllowAccess entity)
        {
            _context.AllowAccesses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var allowAccess = await _context.AllowAccesses.FindAsync(id);
            if (allowAccess != null)
            {
                _context.AllowAccesses.Remove(allowAccess);
                await _context.SaveChangesAsync();
            }
        }
    }
}