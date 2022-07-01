namespace Library.Core.DTO;

public class UserCreationRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string Street { get; set; }
    public string? ApartmentNumber { get; set; }
    public string? BuildingNumber { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }

    public UserCreationRequestDto(string firstName, string lastName, string email, string? phoneNumber, string street, string? apartmentNumber, string? buildingNumber, string city, string zipCode, string country)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Street = street;
        ApartmentNumber = apartmentNumber;
        BuildingNumber = buildingNumber;
        City = city;
        ZipCode = zipCode;
        Country = country;
    }
}