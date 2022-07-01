using ApartmentRental.Infrastructure.Exceptions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly MainContext _mainContext;
    private readonly ILogger<AddressRepository> _logger;

    public AuthorRepository(MainContext mainContext, ILogger<AddressRepository> logger)
    {
        _mainContext = mainContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        var authors = await _mainContext.Author.ToListAsync();

        foreach (var apartment in authors)
        {
            await _mainContext.Entry(apartment).Collection(x => x.Books).LoadAsync();
        }

        return authors;
    }

    public async Task<Author> GetByIdAsync(int id)
    {
        var author = await _mainContext.Author.SingleOrDefaultAsync(x => x.Id == id);
        if (author != null)
        {
            await _mainContext.Entry(author).Collection(x => x.Books).LoadAsync();
            return author;
        }

        _logger.LogError("Cannot find author with provided id: {authorId}", id);
        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Author entity)
    {
        var author = await _mainContext.Author.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (author != null)
        {
            _logger.LogError("Author already exist with Id {authorId}", entity.Id);
            throw new EntityAlreadyExistException();
        }

        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author entity)
    {
        var authorToUpdate = await _mainContext.Author.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (authorToUpdate == null)
        {
            _logger.LogError("Cannot find author with provided id: {authorId}", entity.Id);
            throw new EntityNotFoundException();
        }

        authorToUpdate.FirstName = entity.FirstName;
        authorToUpdate.LastName = entity.LastName;
        authorToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var authorToDelete = await _mainContext.Author.SingleOrDefaultAsync(x => x.Id == id);
        if (authorToDelete != null)
        {
            _mainContext.Author.Remove(authorToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogError("Cannot find author with provided id: {authorId}", id);
            throw new EntityNotFoundException();
        }
    }

    public async Task<int> GetAuthorIdByItsAttributesAsync(string firstName, string lastName)
    {
        var author = await _mainContext.Author.FirstOrDefaultAsync(x =>
            x.FirstName == firstName && x.LastName == lastName);
        return author?.Id ?? 0;
    }

    public async Task<Author> CreateAndGetAsync(Author author)
    {
        author.DateOfCreation = DateTime.UtcNow;
        author.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.AddAsync(author);
        await _mainContext.SaveChangesAsync();

        return author;
    }
}