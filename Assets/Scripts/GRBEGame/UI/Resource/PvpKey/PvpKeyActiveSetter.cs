using GRBEGame.UI.DataView;
using Network.Service;
using Network.Service.Implement;
using UnityEngine;

namespace GRBEGame.UI.Resource.PvpKey
{
    public class PvpKeyActiveSetter : MonoBehaviour, IMemberView<PvpChest>
    {
        
        [SerializeField] private PvpKeyCoreView coreView;
        [SerializeField] private bool activeDefault;
        [SerializeField] private bool activeWhenUpdateView = true;
        
        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(activeDefault);
        }

        public void UpdateView(PvpChest data)
        {
            var response = EndGameServerService.Response;
            if (response.IsError || data.qualityChest is QualityChest.Silver && response.data.IsOpinionQuitPvp() ||
                data.qualityChest is QualityChest.None)
            {
                UpdateDefault();
                return;
            }

            SetActive(activeWhenUpdateView);
        }
        
        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}