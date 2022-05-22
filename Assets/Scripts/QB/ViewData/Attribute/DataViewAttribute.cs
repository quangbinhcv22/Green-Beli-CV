using System;

namespace QB.ViewData
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.ReturnValue | AttributeTargets.Property)]
    public class DataViewAttribute : Attribute
    {
        public readonly string Name;

        public DataViewAttribute(string name)
        {
            Name = name;
        }
    }
}