using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.Property(m=>m.FullName).IsRequired().HasMaxLength(250);
        builder.Property(m=>m.Email).IsRequired().HasMaxLength(200);
        builder.HasIndex(m=>m.Email).IsUnique();
        builder.Property(m=>m.IsActive).HasDefaultValue(true);

        // Global query filter: inactive members are NEVER returned unless
        // the query explicitly calls .IgnoreQueryFilters()
        builder.HasQueryFilter(m => m.IsActive);
    }
}