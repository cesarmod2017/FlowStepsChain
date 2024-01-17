namespace FlowStepsChain.NetCore.Mapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MapperDisplayNameAttribute : Attribute
    {
        public string DisplayName { get; private set; }

        public MapperDisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
