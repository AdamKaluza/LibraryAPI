namespace Library.Core.DTO;

public class BookDto
{
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string YearOfPublication { get; set; }
    public string BookCategory { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }

    public BookDto(string title, string isbn, string yearOfPublication, string bookCategory, string authorFirstName, string authorLastName)
    {
        Title = title;
        ISBN = isbn;
        YearOfPublication = yearOfPublication;
        BookCategory = bookCategory;
        AuthorFirstName = authorFirstName;
        AuthorLastName = authorLastName;
    }
}