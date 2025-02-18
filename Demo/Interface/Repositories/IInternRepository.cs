using Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Interface.Repositories
{
    public interface IInternRepository
    {
        Task<Intern> GetByIdAsync(int id);
        Task<List<Intern>> GetAllAsync();
    }
}