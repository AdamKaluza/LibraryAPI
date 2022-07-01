using FluentAssertions;
using Library.Core.Services;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;
using Moq;

namespace Library.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task GetAllUsersAsync_ShouldNotBeEmpty_WhenUSerColletionIsNotEmpty()
    {
        var users = new List<User>()
        {
            new()
            {
                Address = new Address()
                {
                    City = "Gdańsk",
                    Country = "Poland",
                    Street = "Grunwaldzka",
                    ApartmentNumber = "1",
                    BuildingNumber = "2",
                    ZipCode = "80-600",
                },
                FirstName = "Test",
                LastName = "Adam",
                Email = "test@wp.pl",
                PhoneNumber = "12345678"
            }
        };
        
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
        var service = new UserService(userRepository.Object, Mock.Of<IAddressService>());

        var result = await service.GetAllUsersAsync();
    
        result.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task GetAllUsersAsync_ShouldNotEmpty_WhenUSerColletionIsEmpty()
    {
        var service = new UserService(Mock.Of<IUserRepository>(), Mock.Of<IAddressService>());
        var result = await service.GetAllUsersAsync();
    
        result.Should().BeEmpty();
    }
}