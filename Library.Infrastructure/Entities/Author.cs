namespace Library.Infrastructure.Entities;

public class Author : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public IEnumerable<Book> Books { get; set; }
}