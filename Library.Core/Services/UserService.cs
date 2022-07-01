using Library.Core.DTO;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;

namespace Library.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IAddressService _addressService;

    public UserService(IUserRepository userRepository, IAddressService addressService)
    {
        _userRepository = userRepository;
        _addressService = addressService;
    }

    public async Task AddNewUserAsync(UserCreationRequestDto dto)
    {
        var addressId = await _addressService.CreateAddressAsync(dto.Country,
            dto.City, dto.ZipCode, dto.Street, dto.BuildingNumber, dto.ApartmentNumber);

        await _userRepository.AddAsync(new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressId = addressId
        });
    }

    public async Task UpdateExistingUser(int id, UpdateUserRequestDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);

        await _userRepository.UpdateAsync(new User
        {
            Id = user.Id,
            AddressId = user.AddressId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber
        });
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(x => new UserResponseDto(
            x.Id,
            x.FirstName,
            x.LastName));
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.DeleteByIdAsync(id);
    }
}