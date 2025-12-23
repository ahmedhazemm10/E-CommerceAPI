using E_CommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext appDbContext;
        private readonly DbSet<T> dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            dbSet = this.appDbContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public List<T> GetAll()
        {
            var list = dbSet.ToList();
            return list;
        }

        public T? GetById(int id)
        {
            var entity = dbSet.Find(id);
            return entity;
        }

        public void Save()
        {
            appDbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
