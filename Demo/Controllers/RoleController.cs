using Demo.DTOs.Requests;
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

       await _roleService.CreateRoleAsync(roleRequest);
       return Ok("Role created successfully.");

   }
   [HttpPut("{id?}")]
   public async Task<IActionResult> UpdateRoleAsync(String id, [FromBody] RoleRequest roleRequest)
   {
       await _roleService.UpdateRoleAsync(id, roleRequest);
       return Ok("Role updated successfully.");
   }

   [HttpDelete("{id?}")]
   public async Task<IActionResult> DeleteCourse(String id)
   {
       await _roleService.DeleteRoleAsync(id);
       return Ok("Role deleted successfully.");
   }
}