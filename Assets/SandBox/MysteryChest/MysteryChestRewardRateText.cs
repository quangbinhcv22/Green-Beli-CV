using GRBEGame.UI.DataView;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class MysteryChestRewardRateText : MonoBehaviour, IMemberView<MysteryChestRateResponse>
    {
        [SerializeField] private MysteryChestRewardRateFrameCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string stringDefault;
        [SerializeField] private string stringFormat = "{0}%";
        
        [Header("Text Color")]
        [SerializeField] private Color lowColor;
        [SerializeField] private Color midColor;
        [SerializeField] private Color highColor;


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
            text.SetText(string.Format(stringFormat, data.rate * 100));
            text.color = GetRateColor((int) data.rewardType);
        }

        private const int HighStartIndex = 9;
        private Color GetRateColor(int type)
        {
            if (type <= (int) RewardMysteryType.MISS)
                return lowColor;
            return type >= HighStartIndex ? highColor : midColor;
        }
    }
}
