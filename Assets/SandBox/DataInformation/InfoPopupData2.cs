using System;
using UnityEngine;

namespace SandBox.DataInformation
{
    [Serializable]
    public struct InfoPopupData2
    {
        public static InfoPopupData2 Empty => new InfoPopupData2();
        
        public string title;
        public int titleFontSize;
        [TextArea(5, 999)] public string content;
    }
}
