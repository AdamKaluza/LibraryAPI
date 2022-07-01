namespace Library.Core.DTO;

public class AddBookRequestDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string YearOfPublication { get; set; }
    public int BookCategoryId { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }

    public AddBookRequestDto(string title, string isbn, string yearOfPublication, int bookCategoryId, string authorFirstName, string authorLastName)
    {
        Title = title;
        ISBN = isbn;
        YearOfPublication = yearOfPublication;
        BookCategoryId = bookCategoryId;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
    }
}