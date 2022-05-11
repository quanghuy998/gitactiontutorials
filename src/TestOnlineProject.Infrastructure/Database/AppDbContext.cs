using Microsoft.EntityFrameworkCore;
using TestOnlineProject.Domain.Aggregates.ExamAggregate;
using TestOnlineProject.Domain.Aggregates.QuestionAggregate;
using TestOnlineProject.Domain.Aggregates.RoleAggregate;
using TestOnlineProject.Domain.Aggregates.UserAggregate;

namespace TestOnlineProject.Infrastructure.Database
{
    public sealed class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
