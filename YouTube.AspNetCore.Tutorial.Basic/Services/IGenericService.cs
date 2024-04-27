using System.Linq.Expressions;

namespace YouTube.AspNetCore.Tutorial.Basic.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);
        void CreateItem(TEntity request);
        void UpdateItem(TEntity request);
        void DeleteItem(int id);
        TEntity GetItemById(int id);
    }
}
