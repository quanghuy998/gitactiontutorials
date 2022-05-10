using EcommerceProject.Domain.AggregatesRoot.ProductAggregate;
using EcommerceProject.Domain.AggregatesRoot.RoleAggregate;
using EcommerceProject.Domain.SharedKermel;
using EcommerceProject.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceProject.IntergrationTest.Helpers
{
    public class Utilities
    {
        public static void InittiallizeDbForTests(AppDbContext db)
        {
            //db.Products.AddRange(GetSeedingProduct());
            //db.SaveChanges();
        }

        public static void ReinitializeDbForTests(AppDbContext db)
        {
            db.Products.RemoveRange(db.Products);
            InittiallizeDbForTests(db);
        }
        
        public static List<Role> GetSeedingRole()
        {
            return new List<Role>()
            {
                new Role(UserRole.Admin),
                new Role(UserRole.Customer)
            };
        }

        public static List<Product> GetSeedingProduct()
        {
            return new List<Product>()
            {
                new Product("Product 1", MoneyValue.Of(100, "USA"), "Viet Nam", "Viet Nam", "This is a product 1"),
                new Product("Product 2", MoneyValue.Of(200, "USA"), "Viet Nam", "Viet Nam", "This is a product 2")
            };
        }
    }
}
