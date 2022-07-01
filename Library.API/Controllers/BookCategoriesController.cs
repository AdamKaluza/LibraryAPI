using ApartmentRental.Infrastructure.Exceptions;
using Library.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookCategoriesController : ControllerBase
{
    private readonly IBookCategoryService _bookCategoryService;

    public BookCategoriesController(IBookCategoryService bookCategoryService)
    {
        _bookCategoryService = bookCategoryService;
    }


    [HttpPost("Create")]
    public async Task<IActionResult> CreateNewBookCategoryAsync([FromBody] AddBookCategoryRequestDto dto)
    {
        try
        {
            await _bookCategoryService.CreateBookCategoryAsync(dto);
        }
        catch (EntityAlreadyExistException)
        {
            return BadRequest();
        }
    
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookCategoriesAsync()
    {
        return Ok(await _bookCategoryService.GetAllBookCategoriesAsync());
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookCategoryAsync(int id)
    {
        try
        {
            await _bookCategoryService.DeleteBookCategory(id);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    
        return Ok();
    }
}