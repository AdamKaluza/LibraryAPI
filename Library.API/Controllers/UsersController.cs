using ApartmentRental.Infrastructure.Exceptions;
using Library.Core.DTO;
using Library.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }
    
    [HttpPost("Create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreationRequestDto dto)
    {
        try
        {
            await _userService.AddNewUserAsync(dto);
        }
        catch (EntityAlreadyExistException)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}/Update")]
    public async Task<IActionResult> CreateNewBookAsync([FromBody] UpdateUserRequestDto dto, int id)
    {
        try
        {
            await _userService.UpdateExistingUser(id,dto);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            await _userService.DeleteUser(id);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        return Ok();
    }
}