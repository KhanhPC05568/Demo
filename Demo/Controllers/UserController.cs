using Demo.DTOs.Requests;
using Demo.DTOs.Responses;
using Demo.Interface.Services;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IUserService _userService;

  public UserController(IUserService userService)
  {
    _userService = userService;
  }
  
  [HttpPost]
  [Authorize (Roles = "Admin")]
  public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
  {
    var response = await _userService.CreateUserAsync(request);

    if (response.Status == 1)
    {
      return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
    }

    return Ok(new ApiResponse(response.Status, response.Message, response.Data));
  }

  [HttpPut("{id?}")]
  public async Task<IActionResult> UpdateUserAsync(String id, [FromBody] UserRequest request)
  {
    var response =   await _userService.UpdateUserAsync(id, request);
    if (response.Status == 1)
    {
      return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
    }

    return Ok(new ApiResponse(response.Status, response.Message, response.Data));
  }

  [HttpDelete("{id?}")]
  public async Task<IActionResult> DeleteUser(String id)
  {
    var response =    await _userService.DeleteUserAsync(id);
    if (response.Status == 1)
    {
      return BadRequest(new ApiResponse(response.Status, response.Message,response.Data)); 
    }

    return Ok(new ApiResponse(response.Status, response.Message, response.Data));
  }
  
}