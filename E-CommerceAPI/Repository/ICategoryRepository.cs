using E_CommerceAPI.Models;

namespace E_CommerceAPI.Repository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public List<Category> Search(string name);
    }
}