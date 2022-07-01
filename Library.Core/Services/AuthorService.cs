using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Core.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<int> GetAuthorIdOrCreateAsync(string firstName, string lastname)
    {
        var id = await _authorRepository.GetAuthorIdByItsAttributesAsync(firstName, lastname);
        
        if (id != 0) return id;

        var author = _authorRepository.CreateAndGetAsync(new Author
        {
            FirstName = firstName,
            LastName = lastname
        });

        return author.Id;
    }
}