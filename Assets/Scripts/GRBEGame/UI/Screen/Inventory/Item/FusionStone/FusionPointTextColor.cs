using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FusionPointTextColor : MonoBehaviour, IMemberView<FusionStoneItem>
    {
        [SerializeField] private FusionStoneCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color usedColor;
        [SerializeField] private Color maxColor;
        [SerializeField] private Color defaultColor;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetColor(defaultColor);
        }

        public void UpdateView(FusionStoneItem data)
        {
            SetColor(data.usedPoints < data.maxPoints ? usedColor : maxColor);
        }

        private void SetColor(Color color)
        {
            text.color = color;
        }
    }
}
