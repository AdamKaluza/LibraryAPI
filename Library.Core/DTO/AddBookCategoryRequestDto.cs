namespace Library.Core.Services;

public class AddBookCategoryRequestDto
{
    public string BookCategoryName { get; set; }

    public AddBookCategoryRequestDto(string bookCategoryName)
    {
        BookCategoryName = bookCategoryName;
    }
}