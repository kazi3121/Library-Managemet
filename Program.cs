using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString= "Host=localhost;Port=6022;Username=postgres;Password=12345;Database=libraryManagement";
builder.Services.AddDbContext<LibraryDbContext>(options=>options.UseNpgsql(ConnectionString));
var app = builder.Build();

app.MapPost("/api/books", async (LibraryDbContext context, CreateBookRequest req) =>
{
    var book= new Book
    {
        Title= req.Title,
        Author= req.Author,
        ISBN= req.ISBN,
        AvailableCopies= req.AvailableCopies
    };

    await context.Books.AddAsync(book);
    await context.SaveChangesAsync();

    return Results.Ok(book);
});

app.MapPost("/api/members", async (LibraryDbContext context, CreateMemberRequest req) =>
{
    var member= new Member
    {
        FullName= req.FullName,
        Email= req.Email
    };

    await context.Members.AddAsync(member);
    await context.SaveChangesAsync();

    return Results.Ok(member);
});

app.MapPost("/api/loans", async (LibraryDbContext context, CreateLoanRequest req) =>
{
    var book= await context.Books.FindAsync(req.BookId);

    if(book==null) return Results.NotFound($"The book with id {req.BookId} is not found");
    if(book.AvailableCopies<1) return Results.BadRequest("No copies are currently Availabe");


    var member= await context.Members.FindAsync(req.MemberId);
    if(member==null) return Results.NotFound($"The member with id {req.MemberId} is not found");

    book.AvailableCopies--;

    var loan= new Loan
    {
        BookId=req.BookId,
        MemberId= req.MemberId,
        BorrowedAt= DateTime.UtcNow,
        DueDate= DateTime.UtcNow.AddDays(7)
    };
    await context.Loans.AddAsync(loan);
    await context.SaveChangesAsync();
    
    return Results.Ok(new
    {
        Id= loan.Id,
        BookId     = loan.BookId,
        MemberId   = loan.MemberId,
        BorrowedAt = loan.BorrowedAt,
        DueDate    = loan.DueDate,
        Message    = "Loan created successfully"
    });
});

app.MapGet("/api/loans", async (LibraryDbContext context) =>
{
    var loans = await context.Loans
        .Select(l => new
        {
            l.Id,
            BookTitle = l.Book.Title,
            MemberName = l.Member.FullName,
            l.BorrowedAt,
            l.DueDate
        })
        .ToListAsync();

    return Results.Ok(loans);
});

app.Run();

