using System.Linq.Expressions;

namespace YouTube.AspNetCore.Tutorial.Basic.Services
{
    public interface IGenericService<TEntity,TListVM, TCreateVM, TUpdateVM> 
        where TEntity : class
        where TListVM : class
        where TCreateVM : class
        where TUpdateVM : class
    {
        IList<TListVM> GetAllItems(params Expression<Func<TEntity, object>>[] includeProperties);
        void CreateItem(TCreateVM request);
        void UpdateItem(TUpdateVM request);
        void DeleteItem(int id);
        TUpdateVM GetItemById(int id);
    }
}
