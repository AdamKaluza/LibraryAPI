using Library.Infrastructure.Entities;

namespace Library.Infrastructure.Repositories;

public interface IAddressRepository : IRepository<Address>
{
    Task<Address> CreateAndGetAsync(Address address);
}