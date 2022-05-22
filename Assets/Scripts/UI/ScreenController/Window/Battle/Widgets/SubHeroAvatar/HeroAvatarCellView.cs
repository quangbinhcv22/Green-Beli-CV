using EnhancedUI.EnhancedScroller;
using UI.Widget.HeroCard;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.SubHeroAvatar
{
    public class HeroAvatarCellView : EnhancedScrollerCellView
    {
        [SerializeField] private HeroVisual avatar;

        public void UpdateView(long heroId)
        {
            avatar.UpdateView(heroId);
        }
    }
}