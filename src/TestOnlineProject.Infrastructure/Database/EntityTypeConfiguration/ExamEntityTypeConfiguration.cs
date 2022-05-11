using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal sealed class ExamEntityTypeConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exam");
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Questions).WithMany(p => p.Exams);

            builder.Property(p => p.Title).HasColumnName("Title");
            builder.Property(p => p.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
