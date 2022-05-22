using System;

namespace QB.Collection
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        public TKey key;
        public TValue value;
    }
}