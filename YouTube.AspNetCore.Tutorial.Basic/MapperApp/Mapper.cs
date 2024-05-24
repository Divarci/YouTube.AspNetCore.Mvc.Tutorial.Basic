using Azure.Core;
using Elfie.Serialization;
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

                foreach (var item in sourceList)
                {
                    var destinationOutObject = MapObject(typeof(EntityIn), item,null, typeof(EntityOut));
                    destinationList.Add((EntityOut)destinationOutObject!);
                }

                return (Destination)destinationList;
            }

            var destination = MapObject(typeof(EntityIn), request,null, typeof(EntityOut));

            return (Destination)destination!;
        }

        public Destination Map<Source, Destination>(Source request,Destination outcome) // source is IEnumerable<Category>
        {
            var isCollection = typeof(Source).GetInterfaces().Any(x => x.Name.Contains("ICollection"));
            if (isCollection)
            {
                var sourceList = (ICollection<EntityIn>)request!;

                var destinationObject = Activator.CreateInstance(typeof(Destination));
                var destinationList = (ICollection<EntityOut>)destinationObject!;

                foreach (var item in sourceList)
                {
                    var destinationOutObject = MapObject(typeof(EntityIn),item, outcome, typeof(EntityOut));
                    destinationList.Add((EntityOut)destinationOutObject!);
                }

                return (Destination)destinationList;
            }

            var destination = MapObject(typeof(EntityIn), request, outcome, typeof(EntityOut));

            return (Destination)destination!;
        }

        private object MapObject(Type entityIn,object source,object? outcome, Type entityOut)
        {    

            if(outcome == null)
            {
                PropertyInfo[] sourceProperties = entityIn.GetProperties();
                PropertyInfo[] destinationProperties = entityOut.GetProperties();

                var destination = Activator.CreateInstance(entityOut);
                foreach (var propertyOut in destinationProperties)
                {
                    foreach (var propertyIn in sourceProperties)
                    {
                        if (propertyOut.Name == propertyIn.Name && propertyOut.PropertyType.IsClass && propertyOut.PropertyType != typeof(string))
                        {
                            var nestedValue = propertyIn.GetValue(source);
                            var subDestination = MapObject(propertyIn.PropertyType, nestedValue,null, propertyOut.PropertyType);
                            propertyOut.SetValue(destination, subDestination);
                            break;
                        }


                        if (propertyOut.Name == propertyIn.Name && propertyOut.PropertyType == propertyIn.PropertyType)
                        {
                            var value = propertyIn.GetValue(source);
                            propertyOut.SetValue(destination, value);
                            break;
                        }
                    }
                }
                return destination!;
            }
            else
            {
                PropertyInfo[] sourceProperties = entityIn.GetProperties();
                PropertyInfo[] destinationProperties = entityOut.GetProperties();

                var destination = outcome;
                foreach (var propertyOut in destinationProperties)
                {
                    foreach (var propertyIn in sourceProperties)
                    {
                        if (propertyOut.Name == propertyIn.Name && propertyOut.PropertyType.IsClass && propertyOut.PropertyType != typeof(string))
                        {
                            var nestedValue = propertyIn.GetValue(source);
                            var subDestination = MapObject(propertyIn.PropertyType, nestedValue, destination, propertyOut.PropertyType);
                            propertyOut.SetValue(destination, subDestination);
                            break;
                        }


                        if (propertyOut.Name == propertyIn.Name && propertyOut.PropertyType == propertyIn.PropertyType)
                        {
                            var value = propertyIn.GetValue(source);
                            propertyOut.SetValue(destination, value);
                            break;
                        }
                    }
                }
                return destination!;
            }

           

            
        }
    }
}
