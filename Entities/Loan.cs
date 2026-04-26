public class Loan
{
    public int Id {get; set;}
    public int BookId {get; set;}
    public Book Book{get;set;}
    public int MemberId {get; set;}
    public Member Member {get; set;}
    public DateTime BorrowedAt {get; set;} = DateTime.Now;
    public DateTime DueDate {get; set;}
  
}