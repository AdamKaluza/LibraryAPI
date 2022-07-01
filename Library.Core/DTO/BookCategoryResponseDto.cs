namespace Library.Core.DTO;

public class BookCategoryResponseDto
{
    public string CategoryName { get; set; }

    public BookCategoryResponseDto(string categoryName)
    {
        CategoryName = categoryName;
    }
}