using Library.Infrastructure.Entities;

namespace Library.Infrastructure.Repositories;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Book>> GetAllForUser(int userId);
}