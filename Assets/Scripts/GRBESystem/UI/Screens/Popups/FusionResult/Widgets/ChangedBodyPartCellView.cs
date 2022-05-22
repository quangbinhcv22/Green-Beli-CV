using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GRBESystem.Definitions.BodyPart.Index;
using Network.Messages.GetHeroList;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.FusionResult.Widgets
{
    public class ChangedBodyPartCellView : EnhancedScrollerCellView
    {
        [SerializeField] private List<StatCellView> statCellView;

        public void UpdateView(BodyPartIndex bodyPartIndex, List<HeroResponse> changedHeroes)
        {
            for (int i = 0; i < changedHeroes.Count; i++)
            {
                statCellView[i].UpdateView(bodyPartIndex, changedHeroes[i]);
            }
        }
    }
}