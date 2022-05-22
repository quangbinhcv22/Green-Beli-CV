using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FusionStonePointText : MonoBehaviour, IMemberView<FusionStoneItem>
    {
        [SerializeField] private FusionStoneCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}/{1}";
        [SerializeField] private string stringDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(stringDefault);
        }

        public void UpdateView(FusionStoneItem data)
        {
            text.SetText(string.Format(textFormat, data.usedPoints, data.maxPoints));
        }
    }
}
