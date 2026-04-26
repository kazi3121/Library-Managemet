using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.HasKey(l=>l.Id);

        builder.HasOne(l=>l.Book).WithMany(b=>b.Loans).HasForeignKey(l=>l.BookId).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(l=>l.Member).WithMany(b=>b.Loans).HasForeignKey(l=>l.MemberId).OnDelete(DeleteBehavior.Cascade);
    }
}