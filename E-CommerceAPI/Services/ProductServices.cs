using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using E_CommerceAPI.Repository;

namespace E_CommerceAPI.Services
{
    public class ProductServices : IProductServices
    {
        IProductRepository Repository;
        ICategoryRepository Repositorycat;

        public ProductServices(IProductRepository repository, ICategoryRepository repositorycat)
        {
            Repository = repository;
            Repositorycat = repositorycat;
        }
        public bool Add(ProductCreateDTO productCreateDTO)
        {
            if (Repositorycat.GetById(productCreateDTO.CategoryId) == null)
            {
                return false;
            }
            var product = new Product()
            {
                CategoryId = productCreateDTO.CategoryId,
                Name = productCreateDTO.Name,
                Description = productCreateDTO.Description,
                Quantity = productCreateDTO.Quantity,
                Price = productCreateDTO.Price,
            };
            Repository.Add(product);
            Repository.Save();
            return true;
        }

        public bool Delete(int id)
        {
            var product = Repository.SoftDelete(id);
            if (product == true)
            {
                Repository.Save();
                return true;
            }
            return false;
        }

        public List<ProductReadDTO> FilterByPrice(decimal min, decimal max)
        {
            if (min <= 0 || max <=0 || max < min)
            {
                min = 1;
                max = 1000;
            }
            return Repository.FilterByPrice(min, max).Select(p => new ProductReadDTO()
            {
                CategoryName = p.Category.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ID = p.Id,
                Quantity = p.Quantity
            }).ToList();
        }

        public List<ProductReadDTO> GetAll()
        {
            return Repository.GetProducts().Select(p => new ProductReadDTO()
            {
                CategoryName = p.Category.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ID = p.Id,
                Quantity = p.Quantity
            }).ToList();
        }
        public ProductReadDTO GetById(int id)
        {
            var product = Repository.GetProductWithcategory(id);
            if (product == null)
            {
                return null;
            }
            return new ProductReadDTO()
            {
                CategoryName = product.Category.Name,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ID = product.Id,
                Quantity = product.Quantity
            };
        }

        public List<ProductReadDTO> GetProductsByCategory(int categoryId)
        {
            return Repository.GetProductsByCategory(categoryId).Select(product => new ProductReadDTO()
            {
                CategoryName = product.Category.Name,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ID = product.Id,
                Quantity = product.Quantity
            }).ToList();
        }

        public bool Restore(int id)
        {
            var result = Repository.Restore(id);
            if (result == true)
            {
                Repository.Save();
                return true;
            }
            return false;
        }

        public List<ProductReadDTO> Search(string name)
        {
            return Repository.Search(name).Select(p => new ProductReadDTO()
            {
                CategoryName = p.Category.Name,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ID = p.Id,
                Quantity = p.Quantity
            }).ToList();
        }

        public bool Update(ProductCreateDTO productCreateDTO, int id)
        {
            var product = Repository.GetById(id);
            if (Repositorycat.GetById(productCreateDTO.CategoryId) == null)
            {
                return false;
            }
            if (product != null)
            {
                product.Description = productCreateDTO.Description;
                product.Name = productCreateDTO.Name;
                product.Price = productCreateDTO.Price;
                product.Quantity = productCreateDTO.Quantity;
                product.CategoryId = productCreateDTO.CategoryId;
                Repository.Update(product);
                Repository.Save();
                return true;
            }
            return false;
        }

        public bool UpdateStock(int id, int stock)
        {
            var result = Repository.UpdateStock(id, stock);
            if (result == true) 
            {
                Repository.Save();
                return true; 
            }
            return false;
        }
    }
    public interface IProductServices
    {
        public List<ProductReadDTO> GetAll();
        public ProductReadDTO GetById(int id);
        public bool Add(ProductCreateDTO productCreateDTO);
        public bool Update(ProductCreateDTO productCreateDTO, int id);
        public bool Delete(int id);
        public List<ProductReadDTO> Search(string name);
        public List<ProductReadDTO> FilterByPrice(decimal min, decimal max);
        public List<ProductReadDTO> GetProductsByCategory(int categoryId);
        public bool Restore(int id);
        public bool UpdateStock(int id, int stock);
    }
}