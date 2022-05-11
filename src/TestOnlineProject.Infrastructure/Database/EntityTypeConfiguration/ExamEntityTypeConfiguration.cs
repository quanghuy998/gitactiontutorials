using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;

namespace TestOnlineProject.Infrastructure.Database.EntityTypeConfiguration
{
    internal class ExamEntityTypeConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.ToTable("Exam");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title).HasColumnName("Title");
            builder.Property(p => p.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
