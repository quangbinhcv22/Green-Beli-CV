using GRBESystem.UI.Screens.Windows.ViewHero.Widgets.HeroStatsPanel.Member;
using Network.Messages.GetHeroList;
using UnityEngine;

namespace UI.Widget.HeroCard.Member
{
    public partial class HeroVisualStatPanel : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private HeroVisual ownerVisual;
        [SerializeField] private HeroStatsViewerGroup statsViewerGroup;
        [SerializeField] private HeroResponse defaultHero;

        private void Awake()
        {
            ownerVisual.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            UpdateView(defaultHero);
        }

        public void UpdateView(HeroResponse hero)
        {
            statsViewerGroup.intel.UpdateView(hero.intel);
            statsViewerGroup.growth.UpdateView(hero.growth);
            statsViewerGroup.baseDamage.UpdateView(hero.baseDamage);
            statsViewerGroup.criticalDamage.UpdateViewPercent(hero.critDamageBoot + 1);
            statsViewerGroup.criticalRate.UpdateViewPercent(hero.critRate);
            statsViewerGroup.farming.UpdateViewPercent(hero.farming);
        }
    }

    public partial class HeroVisualStatPanel
    {
        [System.Serializable]
        public struct HeroStatsViewerGroup
        {
            public HeroStatViewer intel, growth, baseDamage, criticalDamage, criticalRate, farming;
        }
    }
}