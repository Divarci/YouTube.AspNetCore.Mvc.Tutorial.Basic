using System.Reflection;

namespace YouTube.AspNetCore.Tutorial.Basic.MapperApp
{
    public class Mapper<EntityIn, EntityOut> : IMapper<EntityIn, EntityOut>
        where EntityIn : class
        where EntityOut : class
    {
        public Destination Map<Source, Destination>(Source request) // source is IEnumerable<Category>
        {
            var isCollection = typeof(Source).GetInterfaces().Any(x => x.Name.Contains("ICollection"));
            if(isCollection)
            {
                var sourceList = (ICollection<EntityIn>)request!;

                var destinationObject = Activator.CreateInstance(typeof(Destination));
                var destinationList = (ICollection<EntityOut>)destinationObject!;

                PropertyInfo[] entityInProps = typeof(EntityIn).GetProperties();
                PropertyInfo[] entityOutProps = typeof(EntityOut).GetProperties();

                foreach (var entityIn in sourceList)
                {
                    var destinationOutObject = Activator.CreateInstance(typeof(EntityOut));

                    foreach (var entityOut in entityOutProps)
                    {
                        var property = entityInProps.FirstOrDefault(x => x.Name == entityOut.Name && x.PropertyType == entityOut.PropertyType);

                        if(property != null)
                        {
                            var value = property.GetValue(entityIn);
                            entityOut.SetValue(destinationOutObject, value);
                        }
                    }

                    destinationList.Add((EntityOut)destinationOutObject!);
                }

                return (Destination)destinationList;
            }

            PropertyInfo[] sourceProperties = typeof(Source).GetProperties();
            PropertyInfo[] destinationProperties = typeof(Destination).GetProperties();

            var destination = Activator.CreateInstance(typeof(Destination));
            foreach (var propertyOut in destinationProperties)
            {
                foreach (var propertyIn in sourceProperties)
                {
                    if(propertyOut.Name == propertyIn.Name && propertyOut.PropertyType == propertyIn.PropertyType)
                    {
                        var value = propertyIn.GetValue(request);
                        propertyOut.SetValue(destination, value);
                    }
                }
            }

            return (Destination)destination!;
        }
    }
}
