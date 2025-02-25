using Demo.Data;
using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ApiResponse> GetAllRole()
    {
        var roles = await _context.Roles
            .Include(r => r.AllowAccesses)
            .ToListAsync();

        var data = roles.Select(r => new
        {
            RoleName = r.RoleName,
            UserName = r.User != null ? r.User.UserName : "No User",
            AllowAccesses = r.AllowAccesses.Select(a => new
            {
                TableName = a.TableName,
                AccessProperties = a.AccessProperties
            }).ToList()
        }).ToList();

        return new ApiResponse(0, "Fill dữ liệu thành công", data);
    }


    public async Task<ApiResponse> CreateRoleAsync(RoleRequest roleRequest)
    {
        var role = new Role
        {
            RoleName = roleRequest.RoleName,
        };
        await _roleRepository.AddAsync(role);

        var roleData = new
        {
            role.RoleId,
            role.RoleName
        };

        return new ApiResponse(0, "Đã tạo role thành công", roleData);
    }


    public async Task<ApiResponse> UpdateRoleAsync(string id, RoleRequest roleRequest)
    {
        if (!int.TryParse(id, out int roleId))
        {
            return new ApiResponse(1, "ID không hợp lệ. Vui lòng kiểm tra lại.", null);
        }

        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            return new ApiResponse(1, "Không tìm thấy vai trò.", null);
        }

        role.RoleName = roleRequest.RoleName;

        await _roleRepository.UpdateAsync(role);
        var roleData = new
        {
            role.RoleId,
            role.RoleName
        };


        return new ApiResponse(0, "Cập nhật thành công.", roleData);
    }


    public async Task<ApiResponse> DeleteRoleAsync(string id)
    {
        if (!int.TryParse(id, out int roleId))
        {
            return new ApiResponse(1, "ID không hợp lệ. Vui lòng kiểm tra lại.", null);
        }

        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
        {
            return new ApiResponse(1, "Không tìm thấy vai trò.", null);
        }

        await _roleRepository.DeleteAsync(roleId);


        return new ApiResponse(0, "Xóa thành công.", null);
    }
}