using System;

namespace QuangBinh.Reflect.Interface
{
    public interface IReflect
    {
        public void Reflect();
    }
    
    public interface IReflectConditionable : IReflect
    {
        public void SetCondition(Func<bool> condition);
    }
}