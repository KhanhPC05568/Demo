using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Demo.Data;
using Demo.DTOs.Requests;
using Demo.Interface.Services;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(ApplicationDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }


    public string Authenticate(LoginRequest request)
    {
        var user = _context.Users.Include(u => u.Role)
            .FirstOrDefault(u => u.FullName == request.FullName);

        if (user == null)
        {
            return null; 
        }

        return GenerateToken(user);
    }
    private string GenerateToken(User user)
    {
        string keyString = _config["JwtSettings:SecretKey"];
        
        if (string.IsNullOrEmpty(keyString))
        {
            throw new Exception("JWT SecretKey is missing in appsettings.json");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));  
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),  // Thêm tên người dùng vào claims
            new Claim("userId", user.UserId.ToString())   // Thêm userId vào claims
        };
        var token = new JwtSecurityToken(
            _config["JwtSettings:Issuer"],
            _config["JwtSettings:Audience"],
            claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}