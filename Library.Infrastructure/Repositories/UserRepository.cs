using ApartmentRental.Infrastructure.Exceptions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MainContext _mainContext;
    private readonly ILogger<AddressRepository> _logger;

    public UserRepository(MainContext mainContext, ILogger<AddressRepository> logger)
    {
        _mainContext = mainContext;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _mainContext.User.ToListAsync();

        foreach (var user in users)
        {
            await _mainContext.Entry(user).Reference(x => x.Address).LoadAsync();
            await _mainContext.Entry(user).Collection(x => x.BooksBorrowed).LoadAsync();
        }

        return users;
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _mainContext.User.SingleOrDefaultAsync(x => x.Id == id);
        if (user != null)
        {
            return user;
        }

        throw new EntityNotFoundException();
    }

    public async Task AddAsync(User entity)
    {
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        var user = await _mainContext.User.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (user == null)
        {
            throw new EntityNotFoundException();
        }

        user.FirstName = entity.FirstName;
        user.LastName = entity.LastName;
        user.Email = entity.Email;
        user.PhoneNumber = entity.PhoneNumber;
        user.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var userToDelete = await _mainContext.User.SingleOrDefaultAsync(x => x.Id == id);
        if (userToDelete != null)
        {
            _mainContext.User.Remove(userToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            throw new EntityNotFoundException();
        }
    }
}