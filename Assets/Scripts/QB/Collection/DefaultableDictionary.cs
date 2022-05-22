using System;
using System.Collections.Generic;

namespace QB.Collection
{
    [Serializable]
    public class DefaultableDictionary<TKey, TValue>
    {
        public TValue defaultValue;
        public List<KeyValuePair<TKey, TValue>> customPairs;

        public TValue this[TKey key] => GetValue(key);


        public DefaultableDictionary()
        {
            customPairs = new List<KeyValuePair<TKey, TValue>>();
        }

        private TValue GetValue(TKey key)
        {
            defaultValue ??= default;
            customPairs ??= new List<KeyValuePair<TKey, TValue>>();

            var customPairIndex = customPairs.FindIndex(pair => pair.key.Equals(key));
            return customPairIndex < (int) default ? defaultValue : customPairs[customPairIndex].value;
        }
    }
}