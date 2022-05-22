using GRBEGame.UI.Screen.Inventory.Fragment;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


namespace GRBEGame.UI.Screen.Inventory
{
    [RequireComponent(typeof(TMP_Text))]
    public class FragmentItemCombineProcessText : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}/{1}";
        [SerializeField] private string defaultString;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.text = defaultString;
        }

        public void UpdateView(FragmentItemInfo data)
        {
            text.SetText(string.Format(textFormat, data.count, data.numberOfRequestsToCombine));
        }

        private void OnValidate()
        {
            Assert.IsNotNull(coreView);
            Assert.IsNotNull(text);
        }
    }
}