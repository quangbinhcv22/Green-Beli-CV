using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ExpCardStarText : MonoBehaviour, IMemberView<ExpCardItem>
    {
        [SerializeField] private ExpCardCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string stringDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(stringDefault);
        }

        public void UpdateView(ExpCardItem data)
        {
            text.SetText(string.Format(textFormat, data.star));
        }
    }
}
