using Manager.Inventory;
using Network.Service;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ButtonInteractableByInsufficientBalanceSetter : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
        [SerializeField] private Button button;
        [SerializeField] private BoxItemType boxItemType;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetInteractable(default);
        }

        public void UpdateView(BoxItemInfo data)
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
            {
                UpdateDefault();
                return;
            }

            SetInteractable(IsValidBalancePack(data));
        }

        private bool IsValidBalancePack(BoxItemInfo data)
        {
            var inventory = NetworkService.Instance.services.loadGameConfig.Response.data.inventory;
            var playerInventory = NetworkService.playerInfo.inventory;

            if (boxItemType != data.boxItemType) return false;
            
            switch (boxItemType)
            {
                case BoxItemType.Box:
                {
                    var price = inventory.GetPriceToUnbox(data.type);
                    return price.gFruit <= playerInventory.GetMoney(MoneyType.GFruit) &&
                           price.grbe <= playerInventory.GetMoney(MoneyType.Grbe);
                }
                case BoxItemType.Pack:
                {
                    var price = inventory.GetPriceToUnpack(data.type);
                    return price.gFruit <= playerInventory.GetMoney(MoneyType.GFruit) &&
                           price.grbe <= playerInventory.GetMoney(MoneyType.Grbe);
                }
                default:
                    return false;
            }
        }

        private void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
        }
    }
}
