using System;
// using Sirenix.OdinInspector;
using UnityEngine;

namespace UIFlow
{
    [Serializable]
    public class UIConfig
    {
        public UIId id;
        // [TableColumnWidth(75, false)]
        public UIType type;
        public LazyLoadReference<UIObject> reference;
    }
}