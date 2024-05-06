using Microsoft.AspNetCore.Mvc.Filters;
using YouTube.AspNetCore.Tutorial.Basic.Exceptions;
using YouTube.AspNetCore.Tutorial.Basic.Generic_Repositories;

namespace YouTube.AspNetCore.Tutorial.Basic.Filters
{
    public class ParameterCheckFilter<TEntity,TUpdateVM> : IActionFilter
        where TEntity : class
        where TUpdateVM : class
    {
        private readonly IGenericRepository<TEntity> _repository;

        public ParameterCheckFilter(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var value = context.ActionArguments.Values.FirstOrDefault();
            if (value == null)
            {
                throw new ClientSideExceptions("Parameter can not be null");
            }

            var intCheck = int.TryParse(value.ToString(), out var intValue);
            if (intCheck)
            {
                var entity = _repository.GetItemById(intValue);
                if (entity == null)
                {
                    throw new ClientSideExceptions("Item not found");
                }
                return;
            }

            var entityCheck = value is TUpdateVM;
            if (!entityCheck)
            {
                throw new ClientSideExceptions("Invalid Object");
            }

            var type = value.GetType();
            var id = type.GetProperties().FirstOrDefault(x=>x.Name == "Id");
            var idValue = id.GetValue(value);
            if (idValue==null)
            {
                throw new ClientSideExceptions("Property Name or Value is invalid");
            }

            var idValueIntCheck = int.TryParse(idValue.ToString(), out var idValueInt);
            if (!idValueIntCheck)
            {
                throw new ClientSideExceptions("Invalid Id");
            }

            var allEntites = _repository.GetAll();
            var idList = new List<int>();

            foreach (var entity in allEntites)
            {
                var tempId = entity.GetType().GetProperties().FirstOrDefault(x => x.Name == "Id");
                var tempIdValue = tempId.GetValue(entity);
                idList.Add((int)tempIdValue);
            }

            if (!idList.Any(x=>x == idValueInt))
            {
                throw new ClientSideExceptions("Item Not Found");
            }

            return;

        }
    }
}
