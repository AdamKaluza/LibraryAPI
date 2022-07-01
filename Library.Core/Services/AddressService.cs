using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Core.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }
    public async Task<int> CreateAddressAsync(string country, string city, string zipCode, string street,
        string buildingNumber, string apartmentNumber)
    {
        var address = await _addressRepository.CreateAndGetAsync(new Address
        {
            Country = country,
            City = city,
            ZipCode = zipCode,
            Street = street,
            BuildingNumber = buildingNumber,
            ApartmentNumber = apartmentNumber
        });

        return address.Id;
    }
}