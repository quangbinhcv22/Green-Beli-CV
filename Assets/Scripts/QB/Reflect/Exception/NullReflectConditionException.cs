using System;

namespace QuangBinh.Reflect.Exception
{
    public class NullReflectConditionException : SystemException
    {
        public const string NormalMessage = "Must use GetInteractCondition before use ReflectInteract";

        public NullReflectConditionException(string message) : base(message)
        {
        }
    }
}
