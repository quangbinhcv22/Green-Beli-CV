using System.Collections.Generic;
using GRBEGame.Define;
using GRBEGame.UI.Screen.Inventory.Fragment;
using Manager.Inventory;
using Network.Service;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentAssembleButtonInteractSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private Button button;

        [SerializeField] private List<FragmentType> ignoreTypeList;


        private void Awake()
        {
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

            SetInteractAble(IsValidAssemble(data));
        }
        
        private bool IsValidAssemble(FragmentItemInfo data)
        {
            var inventory = NetworkService.playerInfo.inventory;
            var assemblePrice = NetworkService.Instance.services.loadGameConfig.Response.data.inventory.GetPriceToAssemble(data.type);
            
            return data.count >= data.numberOfRequestsToCombine &&
                   assemblePrice.gFruit <= inventory.GetMoney(MoneyType.GFruit) &&
                   assemblePrice.grbe <= inventory.GetMoney(MoneyType.Grbe);
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
