using Demo.DTOs.Requests;
using Demo.DTOs.Responses;

namespace Demo.Interface.Services;

public interface IAllowAccessService
{
    Task<ApiResponse> CreateAllowAccessAsync(AllowAccessRequest allowAccessRequest);
    Task<ApiResponse> UpdateAllowAccessAsync(string id, AllowAccessRequest allowAccessRequest);
    Task<ApiResponse> DeleteAllowAccessAsync(string id);
}