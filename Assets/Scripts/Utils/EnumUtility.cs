using System;

namespace Utils
{
    public static class EnumUtility
    {
        public static T Parse<T>(string value)
        {
            return (T) Enum.Parse(typeof(T), value.ToTitleCase(), true);
        }
    }
}