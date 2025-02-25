using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AllowAccessController : ControllerBase
{
   private readonly IAllowAccessService _allowAccessService;

   public AllowAccessController(IAllowAccessService allowAccessService)
   {
      _allowAccessService = allowAccessService;
   }

   [HttpGet]
   [Authorize]
   public async Task<IActionResult> GetAllowAccess()
   {
      var response = await _allowAccessService.GetAllAllowAccess();

      if (response.Status == 1)
      {
         return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
      }

      return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }
   
   
   
   [HttpPost]
   [Authorize]
   public async Task<IActionResult> CreateRole([FromBody] AllowAccessRequest request)
   {
      var response = await _allowAccessService.CreateAllowAccessAsync(request);

      if (response.Status == 1)
      {
         return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
      }

      return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }

   [HttpPut("{id?}")]
   [Authorize]
   public async Task<IActionResult> UpdateRoleAsync(String id, [FromBody] AllowAccessRequest request)
   {
      var response =   await _allowAccessService.UpdateAllowAccessAsync(id, request);
      if (response.Status == 1)
      {
         return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
      }

      return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }

   [HttpDelete("{id?}")]
   [Authorize]
   public async Task<IActionResult> DeleteRole(String id)
   {
      var response =    await _allowAccessService.DeleteAllowAccessAsync(id);
      if (response.Status == 1)
      {
         return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
      }

      return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }
   
   
}