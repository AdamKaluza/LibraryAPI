using Library.Core.DTO;

namespace Library.Core.Services;

public interface IBookService
{
    Task AddBookAsync(AddBookRequestDto dto);
    Task BorrowBookAsync(BorrowBokRequestDto dto);
    Task ReturnBookAsync(int bookId);
    Task<IEnumerable<BookDto>> GetAllAvailableBooksAsync();
    Task<IEnumerable<BookDto>> GetAllUserBorrowedBooksAsync(int userId);

    Task DeleteBookAsync(int id);
}
