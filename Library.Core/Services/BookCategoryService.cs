using ApartmentRental.Infrastructure.Exceptions;
using Library.Core.DTO;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Core.Services;

public class BookCategoryService : IBookCategoryService
{
    private readonly IBookCategoryRepository _bookCategoryRepository;

    public BookCategoryService(IBookCategoryRepository bookCategoryRepository)
    {
        _bookCategoryRepository = bookCategoryRepository;
    }

    public async Task<IEnumerable<BookCategoryResponseDto>> GetAllBookCategoriesAsync()
    {
        var bookCategories = await _bookCategoryRepository.GetAllAsync();

        return bookCategories.Select(x => new BookCategoryResponseDto(
            x.CategoryName));
    }

    public async Task CreateBookCategoryAsync(AddBookCategoryRequestDto dto)
    {
        await _bookCategoryRepository.AddAsync(new BookCategory
        {
            CategoryName = dto.BookCategoryName
        });
    }

    public async Task DeleteBookCategory(int id)
    {
        await _bookCategoryRepository.DeleteByIdAsync(id);
    }
}