using Demo.Data;
using Demo.DTOs.Requests;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;

namespace Demo.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly ApplicationDbContext _context;

    public RoleService(IRoleRepository roleRepository, ApplicationDbContext context)
    {
        _roleRepository = roleRepository;
        _context = context;
    }
   
    public async Task CreateRoleAsync(RoleRequest roleRequest)
    {
        var role = new Role
        {
          RoleName = roleRequest.RoleName,
        };
        await _roleRepository.AddAsync(role);
    }

    public async Task UpdateRoleAsync(string id, RoleRequest roleRequest)
    {
        if (!int.TryParse(id, out int roleId))
            throw new ArgumentException("Invalid role ID");

        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            throw new KeyNotFoundException("Role not found");

        role.RoleName = roleRequest.RoleName;
        
        await _roleRepository.UpdateAsync(role);
    }


    public async Task DeleteRoleAsync(string id)
    {
        if (!int.TryParse(id, out int roleId))
            throw new ArgumentException("Invalid role ID");

        var role = await _roleRepository.GetByIdAsync(roleId);

        await _roleRepository.DeleteAsync(roleId);
    }
}