using Library.Core.DTO;

namespace Library.Core.Services;

public interface IBookCategoryService
{
    Task<IEnumerable<BookCategoryResponseDto>> GetAllBookCategoriesAsync();

    Task CreateBookCategoryAsync(AddBookCategoryRequestDto dto);

    Task DeleteBookCategory(int id);
}