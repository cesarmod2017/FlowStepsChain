using FlowStepsChain.NetCore.Mapper.Attributes;
using FlowStepsChain.NetCore.Mapper.Models;
using System.Reflection;

namespace FlowStepsChain.NetCore.Mapper
{
    public class Mapper
    {
        public static void ToMap<TSource, TDestination>(TSource source, TDestination destination)
        {
            if (source == null || destination == null)
            {
                return;
            }

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = Array.Find(destinationProperties, p => p.Name == sourceProperty.Name);

                if (destinationProperty != null)
                {
                    var sourceValue = sourceProperty.GetValue(source);

                    if (sourceValue != null)
                    {
                        if (sourceProperty.PropertyType.IsClass && !IsPrimitiveOrStructure(sourceProperty.PropertyType))
                        {
                            var destinationValue = destinationProperty.GetValue(destination);

                            if (destinationValue == null)
                            {
                                destinationValue = Activator.CreateInstance(destinationProperty.PropertyType);
                                destinationProperty.SetValue(destination, destinationValue);
                            }

                            ToMap(sourceValue, destinationValue);
                        }
                        else
                        {
                            destinationProperty.SetValue(destination, sourceValue);
                        }
                    }
                }
            }
        }

        private static bool IsPrimitiveOrStructure(Type type)
        {
            return type.IsPrimitive || type == typeof(decimal) || type == typeof(string) || type == typeof(DateTime) || type.IsValueType;
        }

        public static List<MapperChangedProperty> GetChanges(object obj1, object obj2, string parentProperty = null)
        {
            if (obj1 == null || obj2 == null)
            {
                throw new ArgumentNullException("Both objects must be non-null.");
            }

            var changedProperties = new List<MapperChangedProperty>();
            var properties1 = obj1.GetType().GetProperties();
            var properties2 = obj2.GetType().GetProperties();

            foreach (var property1 in properties1)
            {
                var property2 = Array.Find(properties2, p => p.Name == property1.Name);

                if (property2 != null)
                {
                    var value1 = property1.GetValue(obj1);
                    var value2 = property2.GetValue(obj2);

                    if ((value1 == null && value2 != null) || (value1 != null && !value1.Equals(value2)))
                    {
                        string fieldName;
                        var displayNameAttribute = property1.GetCustomAttribute<MapperDisplayNameAttribute>();
                        if (displayNameAttribute != null)
                        {
                            fieldName = displayNameAttribute.DisplayName;
                        }
                        else
                        {
                            fieldName = parentProperty != null ? $"{parentProperty}.{property1.Name}" : property1.Name;
                        }

                        if (property1.PropertyType.IsClass && !property1.PropertyType.Namespace.StartsWith("System") && value1 != null && value2 != null)
                        {
                            changedProperties.AddRange(GetChanges(value1, value2, fieldName));
                        }
                        else
                        {
                            changedProperties.Add(new MapperChangedProperty
                            {
                                Field = fieldName,
                                OldValue = value1?.ToString(),
                                CurrentValue = value2?.ToString()
                            });
                        }
                    }
                }
            }

            return changedProperties;
        }


    }
}
