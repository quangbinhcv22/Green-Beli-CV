using System;
using Network.Messages.GetHeroList;
using TMPro;
using UI.Widget.HeroCard;
using UI.Widget.HeroCard.Member;
using UnityEngine;


namespace GRBESystem.UI.Screens.Windows.RestoreHeroLevel
{
    public class CurrentLevelSelectedHeroText : MonoBehaviour, IHeroVisualMember
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private HeroVisual hero;
        [SerializeField] private string defaultString;


        private void Awake()
        {
            hero.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(defaultString);
        }

        public void UpdateView(HeroResponse heroResponse)
        {
            text.SetText(heroResponse.level.ToString());
        }

        private void OnValidate()
        {
            if (text == null || hero == null) throw new NullReferenceException();
        }
    }
}