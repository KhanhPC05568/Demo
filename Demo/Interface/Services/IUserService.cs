using Demo.DTOs.Requests;
using Demo.DTOs.Responses;

namespace Demo.Interface.Services;

public interface IUserService
{
    Task<ApiResponse> GetAllUser();
    Task<ApiResponse> CreateUserAsync(UserRequest userRequest);
    Task<ApiResponse> UpdateUserAsync(string id, UserRequest userRequest);
    Task<ApiResponse> DeleteUserAsync(string id);
}