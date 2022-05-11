using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Choices).WithOne();

            builder.Property(p => p.QuestionText).HasColumnName("QuestionText");
            builder.Property(p => p.QuestionType).HasColumnName("QuestionType");
            builder.Property(p => p.Point).HasColumnName("Point");
        }
    }
}
