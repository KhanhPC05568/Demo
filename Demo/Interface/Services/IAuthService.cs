using Demo.DTOs.Requests;

namespace Demo.Interface.Services;

public interface IAuthService
{
    string Authenticate(LoginRequest request);
}