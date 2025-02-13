using Demo.DTOs.Requests;

namespace Demo.Interface.Services;

public interface IRoleService
{
    Task CreateRoleAsync(RoleRequest roleRequest);
    Task UpdateRoleAsync(string id, RoleRequest roleRequest);
    Task DeleteRoleAsync(string id);
}
