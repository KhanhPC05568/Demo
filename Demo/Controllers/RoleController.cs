using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
   private readonly IRoleService _roleService;

   public RoleController(IRoleService roleService)
   {
      _roleService = roleService;
   }

   [HttpPost]
   public async Task<IActionResult> CreateRole([FromBody] RoleRequest roleRequest)
   {
       var response = await _roleService.CreateRoleAsync(roleRequest);

       if (response.Status == 1)
       {
           return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
       }

       return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }

   [HttpPut("{id?}")]
   public async Task<IActionResult> UpdateRoleAsync(String id, [FromBody] RoleRequest roleRequest)
   {
       var response =   await _roleService.UpdateRoleAsync(id, roleRequest);
       if (response.Status == 1)
       {
           return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
       }

       return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }

   [HttpDelete("{id?}")]
   public async Task<IActionResult> DeleteRole(String id)
   {
   var response =    await _roleService.DeleteRoleAsync(id);
       if (response.Status == 1)
       {
           return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
       }

       return Ok(new ApiResponse(response.Status, response.Message, response.Data));
   }
}