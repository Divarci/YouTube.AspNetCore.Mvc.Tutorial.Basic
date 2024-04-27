using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;

namespace YouTube.AspNetCore.Tutorial.Basic.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void CreateItem(TEntity request)
        {
            _repository.CreateItem(request);
        }

        public void DeleteItem(int id)
        {
            var item = _repository.GetItemById(id);
            _repository.DeleteItem(item);
        }

        public IList<TEntity> GetAll(params Expression<Func<TEntity, object>>[]? includeProperties)
        {
            var items = _repository.GetAll(); // IQueyable<Product>
            if(includeProperties is not null) // GetAll(x=>x.Categories, x.ProductFeatures)
            {
                foreach(var property in includeProperties)
                {
                    items = items.Include(property); 
                    //GetALl().Include(x=>x.Properties)
                    //GetALl().Include(x=>x.Properties).Include(x=>x.ProductFeatures)
                }
            }

            return items.ToList();  
        }

        public TEntity GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            return item;
        }

        public void UpdateItem(TEntity request)
        {
            _repository.UpdateItem(request);
        }
    }
}
