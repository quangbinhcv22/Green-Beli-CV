using UnityEngine;

namespace UI.ScreenController.Window.Battle.Phrase.MotionText
{
    [System.Serializable]
    public class MotionTextData
    {
        public string titleContent;
        public Color colorText;

        public MotionTextData(string titleContent, Color colorText)
        {
            this.titleContent = titleContent;
            this.colorText = colorText;
        }
    }
}