using Demo.DTOs.Requests;
using Demo.DTOs.Responses;

namespace Demo.Interface.Services;

public interface IRoleService
{
    Task<ApiResponse> GetAllRole();
    Task<ApiResponse> CreateRoleAsync(RoleRequest roleRequest);
    Task<ApiResponse> UpdateRoleAsync(string id, RoleRequest roleRequest);
    Task<ApiResponse> DeleteRoleAsync(string id);
}
