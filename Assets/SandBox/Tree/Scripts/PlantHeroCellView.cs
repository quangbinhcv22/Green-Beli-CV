using EnhancedUI.EnhancedScroller;
using GRBEGame.UI.DataView;
using Network.Messages.GetHeroList;
using QB.ViewData;
using UI.Widget.HeroCard;
using UnityEngine;
using UnityEngine.Events;

public class PlantHeroCellView : EnhancedScrollerCellView
{
    [SerializeField] private HeroVisual heroVisual;

    public void Setup(long heroId)
    {
        heroVisual.UpdateView(heroId);
    }
}

