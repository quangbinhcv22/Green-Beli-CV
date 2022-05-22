using GRBEGame.UI.DataView;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class MysteryChestRewardRateNameText : MonoBehaviour, IMemberView<MysteryChestRateResponse>
    {
        [SerializeField] private MysteryChestRewardRateFrameCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string stringDefault;
        [SerializeField] private string stringFormat = "{0} {1}";


        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(stringDefault);
        }

        public void UpdateView(MysteryChestRateResponse data)
        {
            text.SetText(GetNameOfRate(data));
        }

        private string GetNameOfRate(MysteryChestRateResponse data)
        {
            return data.rewardType switch
            {
                RewardMysteryType.GFRUIT => string.Format(stringFormat, data.quantity, "Gfruit"),
                RewardMysteryType.BELI => string.Format(stringFormat, data.quantity, "BELI"),
                RewardMysteryType.LAND_FRAGMENT => string.Format(stringFormat, data.quantity, "Land"),
                _ => "1 Lucky Point",
            };
        }
    }

    
}
