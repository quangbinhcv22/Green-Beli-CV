using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class InsufficientBalanceFragmentsActiveSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private int fragmentMultiple;


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
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
            {
                UpdateDefault();
                return;
            }

            SetActive(IsValidBalancePack(data));
        }

        private bool IsValidBalancePack(FragmentItemInfo data)
        {
            return fragmentMultiple > data.count;
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
