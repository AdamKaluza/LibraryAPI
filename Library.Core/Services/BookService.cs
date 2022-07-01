using ApartmentRental.Infrastructure.Exceptions;
using Library.Core.DTO;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Core.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorService _authorService;
    private readonly IBookCategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public BookService(IBookRepository bookRepository, IAuthorService authorService, IBookCategoryRepository categoryRepository, IUserRepository userRepository)
    {
        _bookRepository = bookRepository;
        _authorService = authorService;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task AddBookAsync(AddBookRequestDto dto)
    {
        var authorId = await _authorService.GetAuthorIdOrCreateAsync(dto.AuthorFirstName, dto.AuthorLastName);

        var category = await _categoryRepository.GetByIdAsync(dto.BookCategoryId);

        await _bookRepository.AddAsync(new Book
        {
            Title = dto.Title,
            IsBorrowed = false,
            ISBN = dto.ISBN,
            UserId = null,
            YearOfPublication = dto.YearOfPublication,
            BookCategoryId = category.Id,
            AuthorId = authorId
        });
    }

    public async Task BorrowBookAsync(BorrowBokRequestDto dto)
    {
        var book = await _bookRepository.GetByIdAsync(dto.BookId);
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (book.IsBorrowed == true)
        {
            throw new BookAlreadyBorrowedException();
        }
        book.UserId = user.Id;
        book.IsBorrowed = true;
 
        await _bookRepository.UpdateAsync(book);
    }

    public async Task ReturnBookAsync(int bookId)
    {
        var book = await _bookRepository.GetByIdAsync(bookId);
        book.UserId = null;
        book.IsBorrowed = false;
        await _bookRepository.UpdateAsync(book);
    }

    public async Task<IEnumerable<BookDto>> GetAllAvailableBooksAsync()
    {
        var books = await _bookRepository.GetAllAsync();
        return books.Select(x => new BookDto(
            x.Title,
            x.ISBN,
            x.YearOfPublication,
            x.BookCategory.CategoryName,
            x.Author.FirstName,
            x.Author.LastName
        ));
    }

    public async Task<IEnumerable<BookDto>> GetAllUserBorrowedBooksAsync(int userId)
    {
        var books = await _bookRepository.GetAllForUser(userId);
        return books.Select(x => new BookDto(
            x.Title,
            x.ISBN,
            x.YearOfPublication,
            x.BookCategory.CategoryName,
            x.Author.FirstName,
            x.Author.LastName
        ));
    }

    public async Task DeleteBookAsync(int id)
    {
        await _bookRepository.DeleteByIdAsync(id);
    }
}