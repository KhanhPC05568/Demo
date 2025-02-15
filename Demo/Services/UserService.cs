using Demo.Data;
using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;

namespace Demo.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ApplicationDbContext _context;

    public UserService(IUserRepository userRepository, ApplicationDbContext context)
    {
        _userRepository = userRepository;
        _context = context;
    }

    public async Task<ApiResponse> CreateUserAsync(UserRequest userRequest)
    {
        if (userRequest == null)
        {
            return new ApiResponse(1, "Dữ liệu đầu vào không hợp lệ", null);
        }

        var user = new User
        {
            FullName = userRequest.FullName,
            RoleId = userRequest.RoleId,
            DateOfBirth = userRequest.DateOfBirth.Add(DateTime.Now.TimeOfDay).ToUniversalTime(),
            Role = await _context.Roles.FindAsync(userRequest.RoleId) 
        };

await _userRepository.AddAsync(user);
        var userData = new
        {
            user.UserId,
            user.FullName,
            RoleName = user.Role != null ? user.Role.RoleName : "Chưa có vai trò",
            user.DateOfBirth,
        };

        return new ApiResponse(0, "Đã tạo user thành công", userData);
    }


    public async Task<ApiResponse> UpdateUserAsync(string id, UserRequest userRequest)
    {
        if (!int.TryParse(id, out int userId))
        {
            return new ApiResponse(1, "ID không hợp lệ. Vui lòng kiểm tra lại.", null);
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ApiResponse(1, "Không tìm thấy user.", null);
        }
        user.FullName = userRequest.FullName;
        user.RoleId = userRequest.RoleId;
        user.DateOfBirth = userRequest.DateOfBirth.Add(DateTime.Now.TimeOfDay).ToUniversalTime();
        user.Role = await _context.Roles.FindAsync(userRequest.RoleId);
        await _userRepository.UpdateAsync(user);
        
        var userData = new
        {
            user.UserId,
            user.FullName,
            RoleName = user.Role != null ? user.Role.RoleName : "Chưa có vai trò",
            user.DateOfBirth,
        };

        return new ApiResponse(0, "Đã cập nhật user thành công", userData);
        
    }

    public async Task<ApiResponse> DeleteUserAsync(string id)
    {
        if (!int.TryParse(id, out int userId))
        {
            return new ApiResponse(1, "ID không hợp lệ. Vui lòng kiểm tra lại.", null);
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            return new ApiResponse(1, "Không tìm thấy user.", null);
        }

        await _userRepository.DeleteAsync(userId);
        
        return new ApiResponse(0, "Xóa thành công.", null);
    }
}