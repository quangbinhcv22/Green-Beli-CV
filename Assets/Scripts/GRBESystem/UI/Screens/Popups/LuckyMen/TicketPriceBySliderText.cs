using GRBEGame.UI.DataView;
using Network.Service;
using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class TicketPriceBySliderText : MonoBehaviour, IMemberView<float>
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
            if (data <= (float) default ||
                NetworkService.Instance.services.loadGameConfig.Response.data == null)
            {
                text.SetText(textDefault);
                return;
            }

            var price = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;
            text.SetText(string.Format(textFormat, data * price));
        }
    }
}
