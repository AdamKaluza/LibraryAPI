namespace Library.Core.DTO;

public class BorrowBokRequestDto
{
    public int UserId { get; set; }
    public int BookId { get; set; }

    public BorrowBokRequestDto(int UserId, int BookId)
    {
        this.UserId = UserId;
        this.BookId = BookId;
    }
}