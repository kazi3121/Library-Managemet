public class CreateBookRequest
{
    public string Title {get; set;}
    public string Author {get; set;}
    public string ISBN {get; set;}
    public int AvailableCopies {get; set;}
}

public class CreateMemberRequest
{
    public string FullName {get; set;}
    public string Email {get; set;}
}

public class CreateLoanRequest
{
    public int BookId {get; set;}
    public int MemberId {get; set;}
}