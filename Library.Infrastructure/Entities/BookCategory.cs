namespace Library.Infrastructure.Entities;

public class BookCategory : BaseEntity
{
    public string CategoryName { get; set; }
    public IEnumerable<Book> Books { get; set; }
}