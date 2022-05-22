using GRBEGame.UI.Screen.Inventory.Fragment;
using Manager.Inventory;
using Network.Service;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemInsufficientBalanceActiveSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(default);
        }

        public void UpdateView(FragmentItemInfo data)
        {
            var loadGameConfigResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameConfigResponse.IsError)
            {
                UpdateDefault();
                return;
            }
            
            SetActive(IsValidBalanceAssemble(data));
        }
        
        private bool IsValidBalanceAssemble(FragmentItemInfo data)
        {
            var inventory = NetworkService.playerInfo.inventory;
            var assemblePrice = NetworkService.Instance.services.loadGameConfig.Response.data.inventory.GetPriceToAssemble(data.type);
            
            return data.count < data.numberOfRequestsToCombine ||
                   assemblePrice.gFruit > inventory.GetMoney(MoneyType.GFruit) ||
                   assemblePrice.grbe > inventory.GetMoney(MoneyType.Grbe);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
