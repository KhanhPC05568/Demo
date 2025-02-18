using Demo.Models;

namespace Demo.Interface.Repositories;

public interface IAllowAccessRepository : IRepository<AllowAccess>
{
    Task<List<AllowAccess>> GetByRoleIdAsync(int roleId);
}