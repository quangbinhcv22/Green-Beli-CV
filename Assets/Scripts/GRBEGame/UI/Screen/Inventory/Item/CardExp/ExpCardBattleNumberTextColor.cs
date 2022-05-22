using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ExpCardBattleNumberTextColor : MonoBehaviour, IMemberView<ExpCardItem>
    {
        [SerializeField] private ExpCardCoreView coreView;
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

        public void UpdateView(ExpCardItem data)
        {
            SetColor(data.usedBattles < data.maxBattles ? usedColor : maxColor);
        }

        private void SetColor(Color color)
        {
            text.color = color;
        }
    }
}
