using GRBEGame.UI.DataView;
using Manager.Inventory;
using Network.Service;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    public class BuyTicketInsufficientBalance : MonoBehaviour, IMemberView<float>
    {
        [SerializeField] private SliderCoreView coreView;
        [SerializeField] private bool isActiveOnEnoughCost;
        
        [Header("Can none")]
        [SerializeField] private Button button;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            if (button)
                button.interactable = false;
            else
                gameObject.SetActive(false);
        }

        public void UpdateView(float data)
        {
            if (NetworkService.Instance.services.login.IsNotLoggedIn ||  NetworkService.Instance.services.loadGameConfig.Response.IsError)
            {
                UpdateDefault();
                return;
            }
            
            var ticketPrice = NetworkService.Instance.services.loadGameConfig.Response.data.lottery.price;
            var isEnoughCost =
                NetworkService.playerInfo.inventory.GetMoney(MoneyType.GFruit) >= (int) data * ticketPrice;

            if (button)
                button.interactable = isEnoughCost == isActiveOnEnoughCost;
            else
                gameObject.SetActive(isEnoughCost == isActiveOnEnoughCost);
        }
    }
}
