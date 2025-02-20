using Demo.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace  Demo.Sercurity;



public class AdminRoleSecurity : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (user == null || !user.IsInRole("Admin"))
        {
            context.Result = new ObjectResult(new ApiResponse(0, "Tài khoản không có quyền đó", null))
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}