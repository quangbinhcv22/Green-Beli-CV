using System;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

namespace Calculate.Widgets.MutualSubHeroes
{
    public class MutualSubHeroesText : MonoBehaviour
    {
        [SerializeField] private TMP_Text mutualText;
        [SerializeField] private string textFormat;
        [SerializeField] private string textDefault;

        private void Awake()
        {
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
        }

        private void OnEnable()
        {
            OnSelectBattleHeroes();
        }

        private void OnSelectBattleHeroes()
        {
            mutualText.SetText(string.Format(textFormat, MutualCalculator.CalculatePercentSubHeroTotal()));
        }
    }
}