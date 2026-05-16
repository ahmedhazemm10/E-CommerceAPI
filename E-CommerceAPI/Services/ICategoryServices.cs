using E_CommerceAPI.DTOs;

namespace E_CommerceAPI.Services
{
    public interface ICategoryServices
    {
        public List<CategoryReadDTO> GetAll();
        public CategoryReadDTO? GetById(int id);
        public void Add(CategoryDTO entity);
        public bool Update(CategoryDTO entity, int id);
        public bool Delete(int id);
        public List<CategoryReadDTO> Search(string name);
    }
}
