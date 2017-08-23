using DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.CodeFirst.Migrations
{
    public class ApplicationDbInitializer : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public ApplicationDbInitializer()
        {
            AutomaticMigrationsEnabled = true;
            // not allowed migration, if data loss is possible
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            base.Seed(context);

            if (context.Products.Count() == 0)
            {
                Product p1 = new Product("prod1", 3.11);
                Product p2 = new Product("prod2", 23.52);
                Product p3 = new Product("prod3", 355.21);
                Product p4 = new Product("prod4", 37.45);
                Product p5 = new Product("prod5", 123.48);

                context.Products.AddOrUpdate(p5);

                Invoice i1 = new Invoice("Invoice 1", DateTime.Now.AddDays(-2));
                Invoice i2 = new Invoice("Invoice 2", DateTime.Now.AddDays(-50));
                Invoice i3 = new Invoice("Invoice 3", DateTime.Now.AddDays(-10));

                ProductInvoice pi1 = new ProductInvoice(p1, i1, 4);
                ProductInvoice pi2 = new ProductInvoice(p2, i1, 8);
                ProductInvoice pi3 = new ProductInvoice(p3, i1, 16);
                ProductInvoice pi4 = new ProductInvoice(p4, i2, 456);
                ProductInvoice pi5 = new ProductInvoice(p2, i3, 2);
                ProductInvoice pi6 = new ProductInvoice(p3, i3, 1);
                ProductInvoice pi7 = new ProductInvoice(p4, i3, 18);

                context.ProductsInvoices.AddOrUpdate(new[] {pi1,pi2,pi3,pi4,pi5,pi6,pi7});
                context.SaveChanges();

            }
        }
    }
}
