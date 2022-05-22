using GRBEGame.UI.Screen.Inventory.Fragment;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemHighlightProcessCombineText : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color colorHighLight;
        [SerializeField] private Color colorDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }
        
        public void UpdateDefault()
        {
            HighLightText(colorDefault);
        }

        public void UpdateView(FragmentItemInfo data)
        {
            HighLightText(data.count >= data.numberOfRequestsToCombine ? colorHighLight : colorDefault);
        }

        private void HighLightText(Color color)
        {
            text.color = color;
        }
    }
}
