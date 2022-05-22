using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UIFlow.InGame
{
    [RequireComponent(typeof(Button))]
    public class ActiveUIButton : MonoBehaviour
    {
        public UIObject ui;
        public bool isActive = true;
        public bool haveAnimation = true;
        
        private void Awake()
        {
            Assert.IsNotNull(ui);
            GetComponent<Button>().onClick.AddListener(ActiveUI);
        }

        public void ActiveUI()
        {
            if (isActive) ui.Open(haveAnimation: haveAnimation);
            else ui.Close(haveAnimation);
        }
    }
}