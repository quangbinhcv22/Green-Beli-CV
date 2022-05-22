using GRBEGame.UI.Screen.Inventory.Fragment;
using Manager.Inventory;
using Network.Service;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class InsufficientBalancePackActiveSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private Slider slider;
        [SerializeField] private int fragmentMultiple;

        private FragmentItemInfo _fragmentItemInfo;
        

        private void Awake()
        {
            slider.onValueChanged.AddListener(SliderValueChanged);
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(default);
        }

        public void UpdateView(FragmentItemInfo data)
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
            {
                UpdateDefault();
                return;
            }

            _fragmentItemInfo = data;
            SetActive(IsValidBalancePack(data));
        }

        private void SliderValueChanged(float value)
        {
            SetActive(IsValidBalancePack(_fragmentItemInfo));
        }

        private bool IsValidBalancePack(FragmentItemInfo data)
        {
            if (data is null) return false;

            var value = (int) slider.value + 1;
            var fragmentNumber = value * fragmentMultiple;
            var inventory = NetworkService.playerInfo.inventory;
            var price = NetworkService.Instance.services.loadGameConfig.Response.data.inventory.GetPriceToPack(data.type);
            
            return fragmentNumber > data.count ||
                   price.gFruit * value > inventory.GetMoney(MoneyType.GFruit) ||
                   price.grbe * value > inventory.GetMoney(MoneyType.Grbe);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
