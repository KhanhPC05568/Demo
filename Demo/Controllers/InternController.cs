using Demo.DTOs.Responses;
using Demo.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Demo.Services;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternController : ControllerBase
    {
        private readonly IInternService _internService;

        public InternController(IInternService internService)
        {
            _internService = internService;
        }

       
        [HttpGet("getInternData")]
        [Authorize] 
        public async Task<ActionResult<ApiResponse>> GetInternData()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new ApiResponse(1, "Token is required", null));
            }

            
            var response = await _internService.GetInternAsync(token);
            return Ok(response);
        }
    }
}