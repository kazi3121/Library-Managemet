using Microsoft.EntityFrameworkCore;

public class LibraryDbContext: DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options): base(options)
    {
        
    }
    // Register Interceptor here so it fires on every SaveChangesAsync() call
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
    }

    public DbSet<Book> Books {get;set;}
    public DbSet<Member> Members {get;set;}
    public DbSet<Loan> Loans {get;set;}
    public DbSet<AuditLog> AuditLogs {get;set;}
}