using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(b=>b.Title).IsRequired().HasMaxLength(150);
        builder.Property(b=>b.Author).IsRequired().HasMaxLength(250);
        builder.Property(b=>b.ISBN).IsRequired().HasMaxLength(250);
        builder.HasIndex(b=>b.ISBN).IsUnique();
        builder.Property(b=>b.AvailableCopies).HasDefaultValue(true);
    }
}