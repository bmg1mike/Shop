using Shop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;


namespace Shop.DataAccess.InMemory
{
    class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;
        public ProductRepository()
        {
            products = cache["products"] as List<Product>;

            if (products is null)
            {
                products = new List<Product>();
            }

            
        }
        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product product)
        {
            products.Add(product);
        }
        public void Update(Product product)
        {
            var productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate is null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                productToUpdate = product;
            }
        }
        public IEnumerable<Product> Products()
        {
            return products.ToList();
        }
        public Product GetProduct(string id)
        {
            var product = products.Find(p => p.Id == id);
            if(product is null)
            {
                throw new Exception("Product Not Found");
            }
            else
            {
                return product;
            }
        }
        public void Delete(string id)
        {
            var product = products.Find(p => p.Id == id);
            if(product is null)
            {
                throw new Exception("Product not found");
            }
            else
            {
                products.Remove(product);
            }
        }

    }
}
