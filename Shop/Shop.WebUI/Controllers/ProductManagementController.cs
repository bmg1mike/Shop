using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shop.Core.Entities;
using Shop.DataAccess.InMemory;

namespace Shop.WebUI.Controllers
{
    public class ProductManagementController : Controller
    {
        ProductRepository context;

        public ProductManagementController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManagement
        public ActionResult Index()
        {
            var product = context.Products();
            return View(product);
        }
        public ActionResult Create()
        {
            var product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return View("Index");
            }
        }

        public ActionResult Edit(string id)
        {
            var productInDb = context.GetProduct(id);

            if(productInDb is null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productInDb);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            var productInDb = context.GetProduct(id);

            if (productInDb is null)
            {
                return HttpNotFound();
            }
            else
            {
                productInDb.Description = product.Description;
                productInDb.Name = product.Name;
                productInDb.Image = product.Image;
                productInDb.Category = product.Category;
                productInDb.Price = product.Price;
                context.Commit();
                return View("Index");
            }
        }
        public ActionResult Delete(string id)
        {
            var product = context.GetProduct(id);
            if (product is null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            var product = context.GetProduct(id);
            if(product == null)
            {
                return HttpNotFound();
            }
            context.Delete(id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}