namespace Library.Core.Services;

public interface IAddressService
{
    Task<int> CreateAddressAsync(string country, string city, string zipCode, string street, string
        buildingNumber, string apartmentNumber);
}