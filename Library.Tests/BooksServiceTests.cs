using FluentAssertions;
using Library.Core.Services;
using Library.Infrastructure.Entities;
using Library.Infrastructure.Repositories;
using Moq;

namespace Library.Tests;

public class BooksServiceTests
{
    [Fact]
    public async Task GetAllAvailableBooksAsync_ShouldReturnNull_WhenBooksCollectionIsNull()
    {
        var service = new BookService(Mock.Of<IBookRepository>(), Mock.Of<IAuthorService>(),
            Mock.Of<IBookCategoryRepository>(), Mock.Of<IUserRepository>());

        var result = await service.GetAllAvailableBooksAsync();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAvailableBooksAsync_ShouldNotReturnNull_WhenBooksCollectionIsNotNull()
    {
        var books = new List<Book>()
        {
            new()
            {
                Title = "W pustyni i w puszczy",
                IsBorrowed = false,
                ISBN = "123",
                YearOfPublication = "2010",
                BookCategory = new BookCategory()
                {
                    CategoryName = "Przygodowe"
                },
                User = null,
                Author = new Author()
                {
                    FirstName = "Author",
                    LastName = "Anonim"
                }
            },
            new()
            {
                Title = "Hobbit",
                IsBorrowed = true,
                ISBN = "123",
                YearOfPublication = "1998",
                BookCategory = new BookCategory()
                {
                    CategoryName = "Fantasy"
                },
                User = new User()
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
                },
                Author = new Author()
                {
                    FirstName = "Author",
                    LastName = "Anonim"
                }
            }
        };

        var booksRepositoryMock = new Moq.Mock<IBookRepository>();
        booksRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(books);
        var service = new BookService(booksRepositoryMock.Object, Mock.Of<IAuthorService>(),
            Mock.Of<IBookCategoryRepository>(), Mock.Of<IUserRepository>());

        var result = await service.GetAllAvailableBooksAsync();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetBorrowedBooksAsync_ShouldReturnNull_WhenUserDontHaveBorrowedAnyBooks()
    {
        var books = new List<Book>()
        {
            new()
            {
                Title = "W pustyni i w puszczy",
                IsBorrowed = false,
                ISBN = "123",
                YearOfPublication = "2010",
                BookCategory = new BookCategory()
                {
                    CategoryName = "Przygodowe"
                },
                User = null,
                Author = new Author()
                {
                    FirstName = "Author",
                    LastName = "Anonim"
                }
            },
            new()
            {
                Title = "Hobbit",
                IsBorrowed = true,
                ISBN = "123",
                YearOfPublication = "1998",
                BookCategory = new BookCategory()
                {
                    CategoryName = "Fantasy"
                },
                User = new User()
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
                    Id = 2,
                    FirstName = "Test",
                    LastName = "Adam",
                    Email = "test@wp.pl",
                    PhoneNumber = "12345678"
                },
                Author = new Author()
                {
                    FirstName = "Author",
                    LastName = "Anonim"
                }
            }
        };

        var booksRepositoryMock = new Moq.Mock<IBookRepository>();
        booksRepositoryMock.Setup(x => x.GetAllForUser(1)).ReturnsAsync(books);
        var service = new BookService(booksRepositoryMock.Object, Mock.Of<IAuthorService>(),
            Mock.Of<IBookCategoryRepository>(), Mock.Of<IUserRepository>());

        var result = await service.GetAllUserBorrowedBooksAsync(2);
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetBorrowedBooksAsync_ShouldNotReturnNull_WhenUserHaveBorrowedAnyBook()
    {
        var book = new Book()
        {
            Title = "Hobbit",
            IsBorrowed = true,
            ISBN = "123",
            YearOfPublication = "1998",
            BookCategory = new BookCategory()
            {
                CategoryName = "Fantasy"
            },
            User = new User()
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
                Id = 1,
                FirstName = "Test",
                LastName = "Adam",
                Email = "test@wp.pl",
                PhoneNumber = "12345678"
            },
            Author = new Author()
            {
                FirstName = "Author",
                LastName = "Anonim"
            }
        };
        var books = new List<Book>()
        {
            new()
            {
                Title = "W pustyni i w puszczy",
                IsBorrowed = false,
                ISBN = "123",
                YearOfPublication = "2010",
                BookCategory = new BookCategory()
                {
                    CategoryName = "Przygodowe"
                },
                User = new User()
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
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Adam",
                    Email = "test@wp.pl",
                    PhoneNumber = "12345678"
                },
                Author = new Author()
                {
                    FirstName = "Author",
                    LastName = "Anonim"
                }
            },
            book
        };

        var booksRepositoryMock = new Moq.Mock<IBookRepository>();
        booksRepositoryMock.Setup(x => x.GetAllForUser(1)).ReturnsAsync(books);
        var service = new BookService(booksRepositoryMock.Object, Mock.Of<IAuthorService>(),
            Mock.Of<IBookCategoryRepository>(), Mock.Of<IUserRepository>());

        var result = await service.GetAllUserBorrowedBooksAsync(1);
        result.Should().NotBeEmpty();
    }
}