using EnhancedUI.EnhancedScroller;
using Network.Service.Implement;
using UnityEngine;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(MysteryChestRewardRateFrameCoreView))]
    public class MysteryChestRewardRateFrameCellView : EnhancedScrollerCellView
    {
        [SerializeField] private MysteryChestRewardRateFrameCoreView mysteryChestRewardRateFrameCoreView;
        
        
        public void UpdateView(MysteryChestRateResponse mysteryChestRateResponse)
        {
            if (mysteryChestRateResponse is null)
                mysteryChestRewardRateFrameCoreView.UpdateDefault();
            else
                mysteryChestRewardRateFrameCoreView.UpdateView(mysteryChestRateResponse);
        }
    }
}
