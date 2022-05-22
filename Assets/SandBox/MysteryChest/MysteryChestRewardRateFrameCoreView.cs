using GRBEGame.UI.DataView;
using Network.Service.Implement;
using UnityEngine;
using UnityEngine.Events;

namespace SandBox.MysteryChest
{
    public class MysteryChestRewardRateFrameCoreView : MonoBehaviour, ICoreView<MysteryChestRateResponse>
    {
        private UnityAction _defaultCallback;
        private UnityAction<MysteryChestRateResponse> _actionCallback;

        public void UpdateDefault()
        {
            _defaultCallback?.Invoke();
        }

        public void UpdateView(MysteryChestRateResponse data)
        {
            _actionCallback?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<MysteryChestRateResponse> memberView)
        {
            _defaultCallback += memberView.UpdateDefault;
            _actionCallback += memberView.UpdateView;
        }
    }
}
