using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.Assertions;

namespace Manager.Inventory
{
    public class Money : IResource
    {
        private int _value;

        public int Type { get; }
        public int Id { get; }

        public Money(int id)
        {
            Type = ResourceType.Money;
            Id = id;

            _value = 0;
        }

        public int Get()
        {
            return _value;
        }

        public void Set(int value)
        {
            Assert.IsTrue(value >= 0);

            if (_value != value)
            {
                _value = value;
                
                EventManager.EmitEventData(EventName.Inventory.Change, this);
            }
        }

        public void Add(int value)
        {
            Assert.IsTrue(value >= 0);

            _value += value;
            
            EventManager.EmitEventData(EventName.Inventory.Change, this);
            EventManager.EmitEventData(EventName.Inventory.Add, this);
        }

        public void Sub(int value)
        {
            Assert.IsTrue(value >= 0);

            _value = Mathf.Max(_value - value, 0);
            
            EventManager.EmitEventData(EventName.Inventory.Change, this);
            EventManager.EmitEventData(EventName.Inventory.Sub, this);
        }

        public override string ToString()
        {
            return $"Type:{Type}; Id:{Id}; Value: {_value}";
        }
    }
}