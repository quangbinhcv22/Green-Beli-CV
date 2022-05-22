using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace GRBEGame.UI.Screen.Inventory
{
    [RequireComponent(typeof(TMP_Text))]
    public class BoxItemCountText : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string defaultString;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.text = defaultString;
        }

        public void UpdateView(BoxItemInfo data)
        {
            text.SetText($"{data.count}");
        }

        private void OnValidate()
        {
            Assert.IsNotNull(coreView);
            Assert.IsNotNull(text);
        }
    }
}

