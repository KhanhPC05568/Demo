using Demo.DTOs.Responses;

namespace Demo.Interface.Services;

public interface IInternService
{
    Task<ApiResponse> GetInternAsync(string token);
}