using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestOnlineProject.Domain.Aggregates.TestAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class TestEntityTypeConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Test");
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Questions).WithMany(p => p.Tests);

            builder.Property(p => p.Title).HasColumnName("Title");
            builder.Property(p => p.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
