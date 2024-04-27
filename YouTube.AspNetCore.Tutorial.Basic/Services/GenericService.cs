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
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper<TEntity, TListVM> _listMapper;
        private readonly IMapper<TEntity, TCreateVM> _CreateMapper;
        private readonly IMapper<TEntity, TUpdateVM> _UpdateMapper;

        public GenericService(
            IGenericRepository<TEntity> repository, 
            IMapper<TEntity, TListVM> listMapper, 
            IMapper<TEntity, TCreateVM> createMapper, 
            IMapper<TEntity, TUpdateVM> updateMapper)
        {
            _repository = repository;
            _listMapper = listMapper;
            _CreateMapper = createMapper;
            _UpdateMapper = updateMapper;
        }

        public void CreateItem(TCreateVM request)
        {
            var entity = _CreateMapper.Map<TCreateVM,TEntity>(request);
            _repository.CreateItem(entity);
        }

        public void DeleteItem(int id)
        {
            var item = _repository.GetItemById(id);
            _repository.DeleteItem(item);
        }

        public IList<TListVM> GetAllItems(params Expression<Func<TEntity, object>>[]? includeProperties)
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
            var itemListVM = _listMapper.Map<IList<TEntity>, List<TListVM>>(items.ToList());
            return itemListVM;
        }

        public TUpdateVM GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            var updateItem = _UpdateMapper.Map<TEntity, TUpdateVM>(item);
            return updateItem;
        }

        public void UpdateItem(TUpdateVM request)
        {
            var entity = _UpdateMapper.Map<TUpdateVM,TEntity>(request);
            _repository.UpdateItem(entity);
        }
    }
}
