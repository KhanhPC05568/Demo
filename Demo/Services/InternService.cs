using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using Demo.Data;
using Demo.DTOs.Responses;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;
using Microsoft.EntityFrameworkCore;


namespace Demo.Services
{
    public class InternService : IInternService
    {
        private readonly IInternRepository _internRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAllowAccessRepository _allowAccessRepository;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        public InternService(IInternRepository internRepository, IUserRepository userRepository,
            IAllowAccessRepository allowAccessRepository, IConfiguration config, ApplicationDbContext context)
        {
            _internRepository = internRepository;
            _userRepository = userRepository;
            _allowAccessRepository = allowAccessRepository;
            _config = config;
            _context = context;
        }

 public async Task<ApiResponse> GetInternAsync(string token)
{

    var userId = GetUserIdFromToken(token);
    if (userId == null)
    {
        return new ApiResponse(1, "Token is invalid or missing userId.", null);
    }

    var user = await _userRepository.GetByIdAsync(userId.Value);
    if (user == null)
    {
        return new ApiResponse(1, "User not found.", null);
    }

    var roleId = user.RoleId;
    List<AllowAccess> allowAccessList = await _allowAccessRepository.GetByRoleIdAsync(roleId);

    if (allowAccessList == null || !allowAccessList.Any()) 
    {
        return new ApiResponse(1, "No permissions found for the role.", null);
    }

    
    var interns = await _internRepository.GetAllAsync();
    
    var allowedColumns = allowAccessList
        .Where(allowAccess => allowAccess.RoleId == roleId && allowAccess.TableName.Equals("Interns", StringComparison.Ordinal))
        .Select(allowAccess => allowAccess.AccessProperties) 
        .Distinct()
        .ToList();

    if (!allowedColumns.Any())
    {
        return new ApiResponse(1, "No permissions found for the role.", null);
    }
    
    var internResponseList = new List<ExpandoObject>();

    foreach (var intern in interns)
    {
        var internData = new ExpandoObject() as IDictionary<string, object>;
        var internProperties = typeof(Intern).GetProperties();

        foreach (var property in internProperties)
        {
            if (allowedColumns.Contains(property.Name))
            {
                var value = property.GetValue(intern);
                if (value != null && !(value is string str && string.IsNullOrEmpty(str)))
                {
                    internData[property.Name] = value;
                }
            }
        }

        if (internData.Any())
        {
            internResponseList.Add((ExpandoObject)internData);
        }
    }
    
    return new ApiResponse(0, "Lấy dữ liệu thành công", internResponseList);

}




        private int? GetUserIdFromToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "userId"); // Tìm claim "userId"
                if (userIdClaim == null)
                    return null;

                return int.Parse(userIdClaim.Value); // Trả về giá trị của userId trong token
            }
            catch
            {
                return null; 
            }
        }
    }
}