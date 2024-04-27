using Microsoft.EntityFrameworkCore;
using YouTube.AspNetCore.Tutorial.Basic.Context;

namespace YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly CustomContext _customContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(CustomContext customContext)
        {
            _customContext = customContext;
            _dbSet = _customContext.Set<TEntity>();
        }

        public void CreateItem(TEntity request)
        {
            _dbSet.Add(request);
            _customContext.SaveChanges();
        }

        public void DeleteItem(TEntity request)
        {
            _dbSet.Remove(request);
            _customContext.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
            var items = _dbSet.AsQueryable().AsNoTracking();
            return items;
        }

        public TEntity GetItemById(int id)
        {
            var item = _dbSet.Find(id);
            return item;
        }

        public void UpdateItem(TEntity request)
        {
            _dbSet.Update(request);
            _customContext.SaveChanges();
        }
    }
}
