using ApartmentRental.Infrastructure.Exceptions;
using Library.Infrastructure.Context;
using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly MainContext _mainContext;
    private readonly ILogger<AddressRepository> _logger;

    public AddressRepository(MainContext mainContext, ILogger<AddressRepository> logger)
    {
        _mainContext = mainContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Address>> GetAllAsync()
    {
        return await _mainContext.Address.ToListAsync();
        ;
    }

    public async Task<Address> GetByIdAsync(int id)
    {
        var address = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        if (address != null)
        {
            return address;
        }

        _logger.LogError("Cannot find address with provided id: {addressId}", id);
        throw new EntityNotFoundException();
    }

    public async Task AddAsync(Address entity)
    {
        entity.DateOfCreation = DateTime.UtcNow;
        await _mainContext.AddAsync(entity);
        await _mainContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Address entity)
    {
        var addressToUpdate = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == entity.Id);

        if (addressToUpdate == null)
        {
            _logger.LogError("Cannot find address with provided id: {addressId}", entity.Id);
            throw new EntityNotFoundException();
        }

        addressToUpdate.City = entity.City;
        addressToUpdate.Country = entity.Country;
        addressToUpdate.Street = entity.Street;
        addressToUpdate.ApartmentNumber = entity.ApartmentNumber;
        addressToUpdate.BuildingNumber = entity.BuildingNumber;
        addressToUpdate.ZipCode = entity.ZipCode;
        addressToUpdate.DateOfUpdate = DateTime.UtcNow;

        await _mainContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var addressToDelete = await _mainContext.Address.SingleOrDefaultAsync(x => x.Id == id);
        if (addressToDelete != null)
        {
            _mainContext.Address.Remove(addressToDelete);
            await _mainContext.SaveChangesAsync();
        }
        else
        {
            _logger.LogError("Cannot find address with provided id: {addressId}", id);
            throw new EntityNotFoundException();
        }
    }

    public async Task<Address> CreateAndGetAsync(Address address)
    {
        address.DateOfCreation = DateTime.UtcNow;
        address.DateOfUpdate = DateTime.UtcNow;
        await _mainContext.AddAsync(address);
        await _mainContext.SaveChangesAsync();

        return address;
    }
}