using Network.Service;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class PackButtonActiveSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private Button button;
        [SerializeField] private int fragmentMultiple;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetInteractable(default);
        }

        public void UpdateView(FragmentItemInfo data)
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
            {
                UpdateDefault();
                return;
            }

            SetInteractable(IsValidBalancePack(data));
        }

        private bool IsValidBalancePack(FragmentItemInfo data)
        {
            return fragmentMultiple <= data.count;
        }

        private void SetInteractable(bool interactable)
        {
            button.interactable = interactable;
        }
    }
}
