using Demo.Data;
using Demo.Models;
using Demo.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Repositories
{
    public class InternRepository : IInternRepository
    {
        private readonly ApplicationDbContext _context;

        public InternRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Intern> GetByIdAsync(int id)
        {
            return await _context.Interns.FindAsync(id);
        }

        public async Task<List<Intern>> GetAllAsync()
        {
            return await _context.Interns.ToListAsync();
        }
    }
}