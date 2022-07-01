using ApartmentRental.Infrastructure.Exceptions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly MainContext _mainContext;
    private readonly ILogger<AddressRepository> _logger;

    public BookRepository(MainContext mainContext, ILogger<AddressRepository> logger)
    {
        _mainContext = mainContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        var books = await _mainContext.Book.Where(x => !x.IsBorrowed).ToListAsync();

        foreach (var book in books)
        {
            await _mainContext.Entry(book).Reference(x => x.BookCategory).LoadAsync();
            await _mainContext.Entry(book).Reference(x => x.Author).LoadAsync();
            await _mainContext.Entry(book).Reference(x => x.User).LoadAsync();
        }

        return books;
    }

    public async Task<Book> GetByIdAsync(int id)
    {
        var book = await _mainContext.Book.SingleOrDefaultAsync(x => x.Id == id);
        if (book != null)
        {
            await _mainContext.Entry(book).Reference(x => x.BookCategory).LoadAsync();
            await _mainContext.Entry(book).Reference(x => x.Author).LoadAsync();
            await _mainContext.Entry(book).Reference(x => x.User).LoadAsync();
            return book;
        }

        _logger.LogError("Cannot find book  with provided id: {bookId}", id);
        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Book entity)
    {
        var book = await _mainContext.Book.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (book != null)
        {
            _logger.LogError("Book with provided id: {bookId} , already exist", entity.Id);
            throw new EntityAlreadyExistException();
        }

        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book entity)
    {
        var bookToUpdate = await _mainContext.Book.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (bookToUpdate == null)
        {
            _logger.LogError("Cannot find book  with provided id: {bookId}", entity.Id);
            throw new EntityNotFoundException();
        }

        bookToUpdate.Title = entity.Title;
        bookToUpdate.IsBorrowed = entity.IsBorrowed;
        bookToUpdate.ISBN = entity.ISBN;
        bookToUpdate.YearOfPublication = entity.YearOfPublication;
        bookToUpdate.DateOfUpdate = DateTime.UtcNow;
        bookToUpdate.UserId = entity.UserId;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var bookToDelete = await _mainContext.Book.SingleOrDefaultAsync(x => x.Id == id);
        if (bookToDelete != null)
        {
            _mainContext.Book.Remove(bookToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogError("Cannot find book  with provided id: {bookId}", id);
            throw new EntityNotFoundException();
        }
    }

    public async Task<IEnumerable<Book>> GetAllForUser(int userId)
    {
        var books = await _mainContext.Book.Where(x => x.UserId == userId).ToListAsync();

        foreach (var book in books)
        {
            await _mainContext.Entry(book).Reference(x => x.BookCategory).LoadAsync();
            await _mainContext.Entry(book).Reference(x => x.Author).LoadAsync();
            await _mainContext.Entry(book).Reference(x => x.User).LoadAsync();
        }

        return books;
    }
}