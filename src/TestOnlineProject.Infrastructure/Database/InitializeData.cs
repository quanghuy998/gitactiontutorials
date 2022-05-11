using TestOnlineProject.Domain.Aggregates.RoleAggregate;
using TestOnlineProject.Domain.Aggregates.UserAggregate;

namespace TestOnlineProject.Infrastructure.Database
{
    public static class InitializeData
    {
        public static void CreateInitializeData(AppDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                dbContext.Roles.AddRange(CreateInitializeRoles());
                dbContext.SaveChanges();
            }
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(CreateInitializeUser(dbContext));
                dbContext.SaveChanges();
            }
        }

        public static List<Role> CreateInitializeRoles()
        {
            return new List<Role>
            {
                new Role("Admin"),
                new Role("Candidate")
            };
        }

        public static User CreateInitializeUser(AppDbContext dbContext)
        {
            var role = dbContext.Roles.FirstOrDefault(x => x.Name == "Admin");
            return new User("Admin", "admin", "admin@mail.com", role);
        }
    }
}
