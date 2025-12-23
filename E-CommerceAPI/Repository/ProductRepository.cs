using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPI.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public List<Product> FilterByPrice(decimal min, decimal max)
        {
            return appDbContext.Products.Include(p => p.Category).Where(p => p.Price >= min && p.Price <= max).ToList();
        }

        public List<Product> GetProducts()
        {
            return appDbContext.Products.Include(p => p.Category).Where(p => p.IsDeleted == false).ToList();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return appDbContext.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).ToList();
        }

        public bool Restore(int id)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }
            product.IsDeleted = false;
            return true;
        }

        public List<Product> Search(string name)
        {
            return appDbContext.Products.Include(p => p.Category).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public bool SoftDelete(int id)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }
            product.IsDeleted = true;
            return true;
        }

        public bool UpdateStock(int id, int stock)
        {
            var product = GetById(id);
            if (product == null)
            {
                return false;
            }
            product.Quantity = stock;
            return true;
        }
        public Product? GetProductWithcategory(int id)
        {
            return appDbContext.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public int? GetStock(int? id)
        {
            var product = appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            return product.Quantity;
        }
    }
    public interface IProductRepository : IGenericRepository<Product>
    {
        public List<Product> Search(string name);
        public bool SoftDelete(int id);
        public List<Product> FilterByPrice(decimal min, decimal max);
        public List<Product> GetProducts();
        public List<Product> GetProductsByCategory(int categoryId);
        public bool Restore(int id);
        public bool UpdateStock(int id, int stock);
        public Product? GetProductWithcategory(int id);
        public int? GetStock(int? id);
    }
}
