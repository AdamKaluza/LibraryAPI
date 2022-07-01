using Library.Core.DTO;

namespace Library.Core.Services;

public interface IUserService
{
    Task AddNewUserAsync(UserCreationRequestDto dto);
    Task UpdateExistingUser(int id, UpdateUserRequestDto dto);
    Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();

    Task DeleteUser(int id);
}