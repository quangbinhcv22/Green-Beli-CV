using GRBEGame.UI.DataView;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class SliderCountText : MonoBehaviour, IMemberView<float>
    {
        [SerializeField] private SliderCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textDefault;
        [SerializeField] private string textFormat = "{0}";


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(textDefault);
        }

        public void UpdateView(float data)
        {
            text.SetText(string.Format(textFormat, data));
        }
    }
}
