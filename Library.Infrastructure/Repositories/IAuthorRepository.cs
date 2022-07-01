using Library.Infrastructure.Entities;

namespace Library.Infrastructure.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    Task<int> GetAuthorIdByItsAttributesAsync(string firstName, string lastName);

    Task<Author> CreateAndGetAsync(Author author);
}