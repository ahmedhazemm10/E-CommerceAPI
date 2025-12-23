using E_CommerceAPI.DTOs;
using E_CommerceAPI.Models;

namespace E_CommerceAPI.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public List<Category> Search(string name)
        {
            return appDbContext.Categories.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}