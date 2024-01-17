using FlowStepsChain.NetCore.Mapper.Models;

namespace FlowStepsChain.NetCore.Mapper.Extensions
{
    public static class MapperExtensions
    {
        public static void ToMap<TSource, TTarget>(this TSource source, TTarget target = null)
        where TSource : class
        where TTarget : class, new()
        {
            Mapper.ToMap(source, target);
        }

        public static List<MapperChangedProperty> GetChanges<TSource, TTarget>(this TSource source, TTarget target)
        {
            return Mapper.GetChanges(source, target);
        }
    }
}
