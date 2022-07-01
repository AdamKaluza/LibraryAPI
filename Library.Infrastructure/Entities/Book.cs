namespace Library.Infrastructure.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; }
    public bool IsBorrowed { get; set; }
    public string ISBN { get; set; }
    public string YearOfPublication { get; set; }
    
    public int BookCategoryId { get; set; }
    public BookCategory BookCategory { get; set; }
    
    public int? UserId { get; set; }
    public User? User { get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; }
}