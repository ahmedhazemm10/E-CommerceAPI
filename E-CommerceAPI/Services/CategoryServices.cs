using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;
using E_CommerceAPI.Repository;

namespace E_CommerceAPI.Services
{
    public class CategoryServices : ICategoryServices
    {
        ICategoryRepository Repository;

        public CategoryServices(ICategoryRepository repository)
        {
            Repository = repository;
        }

        public void Add(CategoryDTO entity)
        {
            var category = new Category()
            {
                Description = entity.Description,
                Name = entity.Name
            };
            Repository.Add(category);
            Repository.Save();
        }

        public bool Delete(int id)
        {
            var cat = Repository.GetById(id);
            if (cat != null)
            {
                Repository.Delete(cat);
                Repository.Save();
                return true;
            }
            return false;
        }

        public List<CategoryReadDTO> GetAll()
        {
            return Repository.GetAll().Select(x => new CategoryReadDTO() { Description= x.Description, Name=x.Name, Id = x.Id }).ToList();
        }

        public CategoryReadDTO? GetById(int id)
        {
            var cat = Repository.GetById(id);
            if (cat != null)
            {
                var catDTO = new CategoryReadDTO()
                {
                    Description = cat.Description,
                    Name = cat.Name,
                    Id = cat.Id
                };
                return catDTO;
            }
            return null;
        }

        public List<CategoryReadDTO> Search(string name)
        {
            return Repository.Search(name).Select(c => new CategoryReadDTO()
                   {
                      Description = c.Description,
                      Name = c.Name, 
                      Id = c.Id
                   }).ToList();
        }

        public bool Update(CategoryDTO entity, int id)
        {
            var cat = Repository.GetById(id);
            if (cat != null)
            {
                cat.Description = entity.Description;
                cat.Name = entity.Name;
                Repository.Update(cat);
                Repository.Save();
                return true;
            }
            return false;
        }
    }
}
