using System.Collections.Generic;
using GRBEGame.Define;
using GRBEGame.UI.Screen.Inventory.Fragment;
using Manager.Inventory;
using Network.Service;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentPackButtonInteractSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private Button button;
        [SerializeField] private Slider slider;
        [SerializeField] private int fragmentMultiple;
        [SerializeField, Space] private List<FragmentType> ignoreTypeList;

        private FragmentItemInfo _fragmentItemInfo;
        

        private void Awake()
        {
            slider.onValueChanged.AddListener(SliderValueChanged);
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetInteractAble(default);
        }

        public void UpdateView(FragmentItemInfo data)
        {
            if (IsIgnoreType((FragmentType) data.type))
            {
                UpdateDefault();
                return;
            }

            _fragmentItemInfo = data;
            SetInteractAble(IsValidPack(data));
        }
        
        private void SliderValueChanged(float value)
        {
            SetInteractAble(IsValidPack(_fragmentItemInfo));
        }

        private bool IsValidPack(FragmentItemInfo data)
        {
            if (data is null) return false;
             
            const int minValue = 1;
            var fragmentNumber = (int) slider.value + minValue;
            var inventory = NetworkService.playerInfo.inventory;
            var packagePrice =
                NetworkService.Instance.services.loadGameConfig.Response.data.inventory.GetPriceToPack(data.type);
            
            return fragmentMultiple * fragmentNumber <= data.count &&
                   packagePrice.gFruit <= inventory.GetMoney(MoneyType.GFruit) &&
                   packagePrice.grbe <= inventory.GetMoney(MoneyType.Grbe);
        }

        private bool IsIgnoreType(FragmentType fragmentType)
        {
            var isIgnoreType = false;
            ignoreTypeList.ForEach(item =>
            {
                if (item == fragmentType) isIgnoreType = true;
            });

            return isIgnoreType;
        }

        private void SetInteractAble(bool interact)
        {
            button.interactable = interact;
        }
    }
}
