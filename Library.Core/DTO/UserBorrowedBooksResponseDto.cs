namespace Library.Core.DTO;

public class UserBorrowedBooksResponseDto
{
    public IEnumerable<BookDto> BorrowedBooks { get; set; }

    public UserBorrowedBooksResponseDto(IEnumerable<BookDto> borrowedBooks)
    {
        BorrowedBooks = borrowedBooks;
    }
}