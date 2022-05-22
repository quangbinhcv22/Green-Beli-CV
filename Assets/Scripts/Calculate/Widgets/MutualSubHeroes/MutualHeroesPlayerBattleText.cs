using System.Collections.Generic;
using GRBESystem.Definitions;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace Calculate.Widgets.MutualSubHeroes
{
    public class MutualHeroesPlayerBattleText : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        [SerializeField] private TMP_Text buffText;
        [SerializeField] private string textFormat;
        [SerializeField] private bool isDisableOnZeroValue;

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var mutualBuff = MutualCalculator.CalculatePercentSubHeroTotal(GetSelectedHeroes());

            const int zeroValue = 0;
            var isDisableZeroValue = isDisableOnZeroValue && mutualBuff.Equals(zeroValue);

            gameObject.SetActive(isDisableZeroValue == false);
            if (gameObject.activeSelf == false) return;

            buffText.SetText(string.Format(textFormat, mutualBuff) );
        }

        private List<HeroResponse> GetSelectedHeroes()
        {
            return StartGameServerService.Data.GetPlayerInfo(owner).selectedHeros;
        }
    }
}