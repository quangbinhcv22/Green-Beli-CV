using System;
using QuangBinh.Reflect.Exception;
using QuangBinh.Reflect.Interface;
using UnityEngine;

namespace QuangBinh.Reflect
{
    public class ActiveReflector : MonoBehaviour, IReflectConditionable
    {
        private Func<bool> _interactCondition;
        
        
        public void SetCondition(Func<bool> condition)
        {
            _interactCondition = condition;
        }

        public void Reflect()
        {
            if (_interactCondition is null) throw new NullReflectConditionException(NullReflectConditionException.NormalMessage);
            gameObject.SetActive(_interactCondition.Invoke());
        }
    }
}