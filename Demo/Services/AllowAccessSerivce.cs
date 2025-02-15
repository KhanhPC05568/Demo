using Demo.Data;
using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;

namespace Demo.Services;

public class AllowAccessSerivce : IAllowAccessService
{
    private readonly IAllowAccessRepository _allowAccessRepository;
    private readonly ApplicationDbContext _context;


    public AllowAccessSerivce(IAllowAccessRepository allowAccessRepository, ApplicationDbContext context)
    {
        _allowAccessRepository = allowAccessRepository;
        _context = context;
    }

    public async Task<ApiResponse> CreateAllowAccessAsync(AllowAccessRequest allowAccessRequest)
    {
        var allowAccess = new AllowAccess
        {
            AccessProperties = allowAccessRequest.AccessProperties,
            TableName = allowAccessRequest.TableName,
            RoleId = allowAccessRequest.RoleId,
            Role = await _context.Roles.FindAsync(allowAccessRequest.RoleId)
        };
      await  _allowAccessRepository.AddAsync(allowAccess);
        var allowAccessData = new
        {
            allowAccess.AllowAccessId,
            allowAccess.TableName,
            RoleName = allowAccess.Role != null ? allowAccess.Role.RoleName : "Chưa có vai trò",
            allowAccess.AccessProperties
        };
        return new ApiResponse(0, "Đã tạo AllowAccess thành công", allowAccessData);
    }

    public async Task<ApiResponse> UpdateAllowAccessAsync(string id, AllowAccessRequest allowAccessRequest)
    {
        if (!int.TryParse(id, out int allowAccessId))
        {
            return new ApiResponse(1, "ID không hợp lệ. Vui lòng kiểm tra lại.", null);
        }

        var allowAccess= await _allowAccessRepository.GetByIdAsync(allowAccessId);
        if (allowAccess == null)
        {
            return new ApiResponse(1, "Không tìm thấy vai trò.", null);
        }

        allowAccess.AccessProperties = allowAccessRequest.AccessProperties;
        allowAccess.TableName = allowAccessRequest.TableName;
        allowAccess.RoleId = allowAccessRequest.RoleId;
        allowAccess.Role = await _context.Roles.FindAsync(allowAccessRequest.RoleId);
        var allowAccessData = new
        {
            allowAccess.AllowAccessId,
            allowAccess.TableName,
            RoleName = allowAccess.Role != null ? allowAccess.Role.RoleName : "Chưa có vai trò",
            allowAccess.AccessProperties
        };
        return new ApiResponse(0, "Đã cập nhật AllowAccess thành công", allowAccessData);
    }

    public async Task<ApiResponse> DeleteAllowAccessAsync(string id)
    {
        if (!int.TryParse(id, out int allowAccessId))
        {
            return new ApiResponse(1, "ID không hợp lệ. Vui lòng kiểm tra lại.", null);
        }

        var allowAccess= await _allowAccessRepository.GetByIdAsync(allowAccessId);
        if (allowAccess == null)
        {
            return new ApiResponse(1, "Không tìm thấy vai trò.", null);
        }
        await _allowAccessRepository.DeleteAsync(allowAccessId);
        return new ApiResponse(0, "Xóa thành công.", null);
    }
}