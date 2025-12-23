using E_CommerceAPI.DTOs;

namespace E_CommerceAPI.Services
{
    public interface ICategoryServices
    {
        public List<CategoryDTO> GetAll();
        public CategoryDTO? GetById(int id);
        public void Add(CategoryDTO entity);
        public bool Update(CategoryDTO entity, int id);
        public bool Delete(int id);
        public List<CategoryDTO> Search(string name);
    }
}
