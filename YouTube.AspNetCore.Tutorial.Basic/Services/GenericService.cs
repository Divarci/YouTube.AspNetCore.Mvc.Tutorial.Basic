using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;
using YouTube.AspNetCore.Tutorial.Basic.MapperApp;

namespace YouTube.AspNetCore.Tutorial.Basic.Services
{
    public class GenericService<TEntity, TListVM, TCreateVM, TUpdateVM> : IGenericService<TEntity, TListVM, TCreateVM, TUpdateVM>
        where TEntity : class
        where TListVM : class
        where TCreateVM : class
        where TUpdateVM : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual void CreateItem(TCreateVM request)
        {
            var entity = _mapper.Map<TCreateVM,TEntity>(request,3);
            _repository.CreateItem(entity);
        }

        public virtual void DeleteItem(int id)
        {
            var item = _repository.GetItemById(id);
            _repository.DeleteItem(item);
        }

        public virtual IList<TListVM> GetAllItems(params Expression<Func<TEntity, object>>[]? includeProperties)
        {
            var items = _repository.GetAll(); // IQueyable<Product>
            if (includeProperties is not null) // GetAll(x=>x.Categories, x.ProductFeatures)
            {
                foreach (var property in includeProperties)
                {
                    items = items.Include(property);
                    //GetALl().Include(x=>x.Properties)
                    //GetALl().Include(x=>x.Properties).Include(x=>x.ProductFeatures)
                }
            }
            var itemListVM = _mapper.Map<IList<TEntity>, List<TListVM>>(items.ToList(),3);
            return itemListVM;
        }

        public virtual TUpdateVM GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            var updateItem = _mapper.Map<TEntity, TUpdateVM>(item, 3);
            return updateItem;
        }

        public virtual void UpdateItem(TUpdateVM request)
        {
            var entity = _mapper.Map<TUpdateVM,TEntity>(request,3);
            _repository.UpdateItem(entity);
        }
    }
}
