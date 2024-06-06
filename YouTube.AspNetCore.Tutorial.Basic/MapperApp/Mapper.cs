using System.Collections;
using YouTube.AspNetCore.Tutorial.Basic.Exceptions;

namespace YouTube.AspNetCore.Tutorial.Basic.MapperApp
{
    public class Mapper : IMapper
    {
        private const string Collection = "Collection";
        private const string SingleUnit = "SingleUnit";

        public Destination Map<Source, Destination>(Source request, int depth)
        {
            if (request is null) throw new MapperException("Mapper source can not be null");
            

            var destination = MapObject(typeof(Source), request, null, typeof(Destination),depth,null);
            return (Destination)destination!;
        }

        public Destination Map<Source, Destination>(Source request, Destination outcome,int depth)
        {
            if (request is null) throw new MapperException("Mapper source can not be null");
            if (outcome is null) throw new MapperException("Mapper destination can not be null");

            var destination = MapObject(typeof(Source), request, outcome, typeof(Destination), depth,null);
            return (Destination)destination!;
        }

        private object MapObject(Type sourceType, object source, object? outcome, Type destinationType,int depth,string? previousProcess)
        {
            if(source is null) throw new MapperException("Mapper source can not be null");
            if (depth <= 0) return null;
            if (previousProcess is Collection) depth++;

            var isCollection = typeof(IEnumerable).IsAssignableFrom(sourceType) && sourceType != typeof(string);
            if (isCollection)
            {
                return CollectionMapper(source, outcome, destinationType, ref depth);
            }

            return SingleUnitMapper(sourceType, source, outcome, destinationType, ref depth);

        }

        private object SingleUnitMapper(Type sourceType, object source, object? outcome, Type destinationType,ref int depth)
        {
            var sourceProperties = sourceType.GetProperties().ToDictionary(p => p.Name);
            var destinationProperties = destinationType.GetProperties();

            var destination = outcome ?? Activator.CreateInstance(destinationType);
            foreach (var destinationProperty in destinationProperties)
            {
                if (!sourceProperties.TryGetValue(destinationProperty.Name, out var sourceProperty))
                    continue;

                if (destinationProperty.PropertyType.IsClass && destinationProperty.PropertyType != typeof(string))
                {
                    var nestedValue = sourceProperty.GetValue(source);
                    depth--;
                    var subDestination = MapObject(sourceProperty.PropertyType, nestedValue, outcome, destinationProperty.PropertyType,depth,SingleUnit);
                    depth++;
                    destinationProperty.SetValue(destination, subDestination);
                    continue;
                }

                if (destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    var value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                    continue;
                }
            }
            return destination!;
        }

        private object CollectionMapper(object source, object? outcome, Type destinationType,ref int depth)
        {
            if (source is not IEnumerable sourceList)
                throw new MapperException($"Source object of type is not an IEnumerable");

            var destinationObject = Activator.CreateInstance(destinationType);
            var destinationList = (IList)destinationObject!;

            var typeOfDestinationObject = destinationType.GetGenericArguments()[0];

            foreach (var item in sourceList)
            {
                depth--;
                var destinationOutObject = MapObject(item.GetType(), item, outcome, typeOfDestinationObject,depth,Collection);
                depth++;
                destinationList.Add(destinationOutObject);
            }
            return destinationList;
        }
    }
}

