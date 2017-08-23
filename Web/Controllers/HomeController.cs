using DataBase.Entities;
using EntityFramework.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Product> allProducts = new List<Product>();

            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                allProducts = context.Products.ToList();
            }

            //ViewData["PageSize"] = 2;
            ViewData["PageNumber"] = (allProducts.Count() + 2 - 1) / 2; //int pages = (total + pageSize - 1)/pageSize;
            ViewData["CurrentPage"] = 1;
            return View(allProducts.Skip(0 * 2).Take(2).ToList());
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = new Product();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                product = context.Products.Where(p => p.ID == id).FirstOrDefault();
            }
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = new Product();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                product = context.Products.Where(p => p.ID == id).FirstOrDefault();
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();

                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    product = context.Products.Where(p => p.ID == model.ID).FirstOrDefault();
                    if (product != null)
                    {
                        product.Name = model.Name;
                        context.Entry(product).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public PartialViewResult Search(string searchString, int currentPage, int pageNumber)
        {
            List<Product> products = new List<Product>();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {

                if (!string.IsNullOrEmpty(searchString))
                {
                    products = context.Products
                        .Where(p => p.Name.ToLower().Contains(searchString.ToLower())
                        || p.Price.ToString().ToLower().Contains(searchString.ToLower())).ToList();


                    if (pageNumber != ((products.Count() + 2 - 1) / 2))
                    {
                        currentPage = 1;
                        pageNumber = (products.Count() + 2 - 1) / 2;
                    }
                }
                else
                {
                    products = context.Products.ToList();
                }
            }

            ViewData["PageNumber"] = pageNumber;
            ViewData["CurrentPage"] = currentPage;
            return PartialView("~/Views/Home/_Table.cshtml", products.Skip((currentPage-1) * 2).Take(2).ToList());
        }
    }
}