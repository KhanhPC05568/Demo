using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using Demo.DTOs.Responses;
using Demo.Interface.Repositories;
using Demo.Interface.Services;
using Demo.Models;


namespace Demo.Services
{
    public class InternService : IInternService
    {
        private readonly IInternRepository _internRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAllowAccessRepository _allowAccessRepository;
        private readonly IConfiguration _config;

        public InternService(IInternRepository internRepository, IUserRepository userRepository,
            IAllowAccessRepository allowAccessRepository, IConfiguration config)
        {
            _internRepository = internRepository;
            _userRepository = userRepository;
            _allowAccessRepository = allowAccessRepository;
            _config = config;
        }

 public async Task<ApiResponse> GetInternAsync(string token)
{
    // Extract userId from the token
    var userId = GetUserIdFromToken(token);
    if (userId == null)
    {
        return new ApiResponse(1, "Token is invalid or missing userId.", null);
    }

    // Get user information based on userId extracted from token
    var user = await _userRepository.GetByIdAsync(userId.Value);
    if (user == null)
    {
        return new ApiResponse(1, "User not found.", null);
    }

    var roleId = user.RoleId;
    List<AllowAccess> allowAccessList = await _allowAccessRepository.GetByRoleIdAsync(roleId);

    if (allowAccessList == null || !allowAccessList.Any()) // Kiểm tra nếu không có quyền truy cập
    {
        return new ApiResponse(1, "No permissions found for the role.", null);
    }

    // Get interns list
    var interns = await _internRepository.GetAllAsync();

    // Get the list of allowed columns based on the permissions of the role
    var allowedColumns = allowAccessList
        .Where(allowAccess => allowAccess.RoleId == roleId) // Lọc theo RoleId
        .Select(allowAccess => allowAccess.TableName)       // Chỉ lấy các TableName được phép
        .ToList();

    // Prepare filtered intern data based on allowed columns
    var filteredInterns = interns.Select(intern =>
    {
        var internData = new ExpandoObject() as IDictionary<string, object>;

        // Check if the allowedColumns contains the relevant fields and add them to internData
        if (allowedColumns.Contains("Id"))
        {
            internData.Add("Id", intern.Id);
        }

        if (allowedColumns.Contains("InternName"))
        {
            internData.Add("InternName", intern.InternName);
        }

        if (allowedColumns.Contains("InternAddress"))
        {
            internData.Add("InternAddress", intern.InternAddress);
        }

        if (allowedColumns.Contains("ImageData"))
        {
            internData.Add("ImageData", intern.ImageData);
        }

        if (allowedColumns.Contains("DateOfBirth"))
        {
            internData.Add("DateOfBirth", intern.DateOfBirth);
        }

        if (allowedColumns.Contains("InternMail"))
        {
            internData.Add("InternMail", intern.InternMail);
        }

        if (allowedColumns.Contains("InternMailReplace"))
        {
            internData.Add("InternMailReplace", intern.InternMailReplace);
        }

        if (allowedColumns.Contains("University"))
        {
            internData.Add("University", intern.University);
        }

        if (allowedColumns.Contains("CitizenIdentification"))
        {
            internData.Add("CitizenIdentification", intern.CitizenIdentification);
        }

        if (allowedColumns.Contains("Major"))
        {
            internData.Add("Major", intern.Major);
        }

        if (allowedColumns.Contains("FullTime"))
        {
            internData.Add("FullTime", intern.FullTime);
        }

        if (allowedColumns.Contains("Cvfile"))
        {
            internData.Add("Cvfile", intern.Cvfile);
        }

        if (allowedColumns.Contains("InternSpecialized"))
        {
            internData.Add("InternSpecialized", intern.InternSpecialized);
        }

        if (allowedColumns.Contains("TelephoneNum"))
        {
            internData.Add("TelephoneNum", intern.TelephoneNum);
        }

        if (allowedColumns.Contains("InternStatus"))
        {
            internData.Add("InternStatus", intern.InternStatus);
        }

        if (allowedColumns.Contains("RegisteredDate"))
        {
            internData.Add("RegisteredDate", intern.RegisteredDate);
        }

        if (allowedColumns.Contains("HowToKnowAlta"))
        {
            internData.Add("HowToKnowAlta", intern.HowToKnowAlta);
        }

        if (allowedColumns.Contains("InternPassword"))
        {
            internData.Add("InternPassword", intern.InternPassword);
        }

        if (allowedColumns.Contains("ForeignLanguage"))
        {
            internData.Add("ForeignLanguage", intern.ForeignLanguage);
        }

        if (allowedColumns.Contains("YearOfExperiences"))
        {
            internData.Add("YearOfExperiences", intern.YearOfExperiences);
        }

        if (allowedColumns.Contains("PasswordStatus"))
        {
            internData.Add("PasswordStatus", intern.PasswordStatus);
        }

        if (allowedColumns.Contains("ReadyToWork"))
        {
            internData.Add("ReadyToWork", intern.ReadyToWork);
        }

        if (allowedColumns.Contains("InternEnabled"))
        {
            internData.Add("InternEnabled", intern.InternEnabled);
        }

        if (allowedColumns.Contains("EntranceTest"))
        {
            internData.Add("EntranceTest", intern.EntranceTest);
        }

        if (allowedColumns.Contains("Introduction"))
        {
            internData.Add("Introduction", intern.Introduction);
        }

        if (allowedColumns.Contains("Note"))
        {
            internData.Add("Note", intern.Note);
        }

        if (allowedColumns.Contains("LinkProduct"))
        {
            internData.Add("LinkProduct", intern.LinkProduct);
        }

        if (allowedColumns.Contains("JobFields"))
        {
            internData.Add("JobFields", intern.JobFields);
        }

        if (allowedColumns.Contains("HiddenToEnterprise"))
        {
            internData.Add("HiddenToEnterprise", intern.HiddenToEnterprise);
        }

        return internData;
    }).ToList();

    return new ApiResponse(0, "Lấy thông tin thành công.", filteredInterns);
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
                return null; // Trả về null nếu có lỗi trong quá trình đọc token
            }
        }
    }
}