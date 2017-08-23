using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Entities
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public double Price { get; set; }

        public virtual List<ProductInvoice> ProductsInvoices { get; set; }

        public Product()
        {
            ProductsInvoices = new List<ProductInvoice>();
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
            ProductsInvoices = new List<ProductInvoice>();
        }
    }
}
