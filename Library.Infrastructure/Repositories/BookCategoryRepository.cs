using ApartmentRental.Infrastructure.Exceptions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;

public class BookCategoryRepository : IBookCategoryRepository
{
    private readonly MainContext _mainContext;
    private readonly ILogger<AddressRepository> _logger;

    public BookCategoryRepository(MainContext mainContext, ILogger<AddressRepository> logger)
    {
        _mainContext = mainContext;
        _logger = logger;
    }

    public async Task<IEnumerable<BookCategory>> GetAllAsync()
    {
        var bookCategories = await _mainContext.BookCategory.ToListAsync();

        foreach (var book in bookCategories)
        {
            await _mainContext.Entry(book).Collection(x => x.Books).LoadAsync();
        }

        return bookCategories;
    }

    public async Task<BookCategory> GetByIdAsync(int id)
    {
        var bookCategory = await _mainContext.BookCategory.SingleOrDefaultAsync(x => x.Id == id);
        if (bookCategory != null)
        {
            await _mainContext.Entry(bookCategory).Collection(x => x.Books).LoadAsync();
            return bookCategory;
        }

        _logger.LogError("Cannot find book category with provided id: {bookCategoryId}", id);
        throw new EntityNotFoundException();
    }

    public async Task AddAsync(BookCategory entity)
    {
        var bookCategory = await _mainContext.BookCategory.SingleOrDefaultAsync(x => x.CategoryName == entity.CategoryName);

        if (bookCategory != null)
        {
            _logger.LogError("Book category already exist with provides name: {bookCategoryName}", entity.CategoryName);
            throw new EntityAlreadyExistException();
        }

        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(BookCategory entity)
    {
        var bookCategoryToUpdate = await _mainContext.BookCategory.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (bookCategoryToUpdate == null)
        {
            _logger.LogError("Cannot find book category with provided id: {bookCategoryId}", entity.Id);
            throw new EntityNotFoundException();
        }

        bookCategoryToUpdate.CategoryName = entity.CategoryName;
        bookCategoryToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var apartmentToDelete = await _mainContext.BookCategory.SingleOrDefaultAsync(x => x.Id == id);
        if (apartmentToDelete != null)
        {
            _mainContext.BookCategory.Remove(apartmentToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogError("Cannot find book category with provided id: {bookCategoryId}", id);
            throw new EntityNotFoundException();
        }
    }
}