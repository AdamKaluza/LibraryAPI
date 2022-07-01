using ApartmentRental.Infrastructure.Exceptions;
using Library.Core.DTO;
using Library.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

   
    [HttpPost("Create")]
    public async Task<IActionResult> CreateNewBookAsync([FromBody] AddBookRequestDto dto)
    {
        try
        {
            await _bookService.AddBookAsync(dto);
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }
    
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAvailableBooks()
    {
        return Ok(await _bookService.GetAllAvailableBooksAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBorrowedBooksByUser(int id)
    {
        return Ok(await _bookService.GetAllUserBorrowedBooksAsync(id));
    }
    
    [HttpPut("Borrow")]
    public async Task<IActionResult> BorrowBookAsync([FromBody] BorrowBokRequestDto dto)
    {
        try
        {
            await _bookService.BorrowBookAsync(dto);
        }
        catch (EntityNotFoundException)
        {
            return BadRequest();
        }
        catch (BookAlreadyBorrowedException)
        {
            return BadRequest();
        }
 
        return Ok();
    }
    
    [HttpPut("{id}/Return")]
    public async Task<IActionResult> ReturnBookAsync(int id)
    {
        try
        {
            await _bookService.ReturnBookAsync(id);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
 
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        try
        {
            await _bookService.DeleteBookAsync(id);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
 
        return Ok();
    }

}