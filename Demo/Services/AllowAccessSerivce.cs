using Demo.Data;
using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ApiResponse> GetAllAllowAccess()
    {
    
            var allowAccesses = await _context.AllowAccesses.Include(c => c.Role).ToListAsync();
            var data = allowAccesses.Select(c => new 
            {
                RoleName = c.Role != null ? c.Role.RoleName : "No Role",
                TableName = c.TableName,
                AccessProperties = c.AccessProperties
            }).ToList();

            return new ApiResponse(0, "Fill dữ liệu thành công", data);
    
    }


    public async Task<ApiResponse> CreateAllowAccessAsync(AllowAccessRequest allowAccessRequest)
    {
       
        var role = await _context.Roles.FindAsync(allowAccessRequest.RoleId);
        if (role == null)
        {
            return new ApiResponse(1, "Vai trò không tồn tại." , null);
        }

  
        var resultData = new List<object>();

        var accessPropertiesList = allowAccessRequest.AccessProperties.Split(',');
        var addedProperties = new List<string>();
        foreach (var accessProperty in accessPropertiesList)
        {
            var existingAllowAccess = await _context.AllowAccesses
                .FirstOrDefaultAsync(a => a.TableName == allowAccessRequest.TableName &&
                                          a.RoleId == allowAccessRequest.RoleId &&
                                          a.AccessProperties == accessProperty.Trim());

            if (existingAllowAccess != null)
            {
                continue; 
            }
          
            var allowAccess = new AllowAccess
            {
                AccessProperties = accessProperty.Trim(),
                TableName = allowAccessRequest.TableName,
                RoleId = allowAccessRequest.RoleId,
                Role = role
            };
            
            await _allowAccessRepository.AddAsync(allowAccess);
            addedProperties.Add(accessProperty);
            resultData.Add(new
            {
                allowAccess.AllowAccessId,
                allowAccess.TableName,
                RoleName = allowAccess.Role != null ? allowAccess.Role.RoleName : "Chưa có vai trò",
                AccessProperties = allowAccess.AccessProperties
            });
        }
        if (addedProperties.Count == 0)
        {
            return new ApiResponse(1, "Dữ liệu đã có trong bảng", null);
        }
        return new ApiResponse(0, "Đã tạo AllowAccess thành công", resultData);
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
        
     await  _allowAccessRepository.UpdateAsync(allowAccess);
        
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