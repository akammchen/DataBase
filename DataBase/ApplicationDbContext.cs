using System.Data.Entity;
using EntityFramework.CodeFirst.Migrations;
using DataBase.Entities;

namespace EntityFramework.CodeFirst
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=MyConnectionString")
        {
            //Important performance code
            Configuration.AutoDetectChangesEnabled = false; 
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, ApplicationDbInitializer>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ProductInvoice> ProductsInvoices { get; set; }
        // public DbSet<MyClass> Educations { get; set; } //Example for creating db table 
    }
}
