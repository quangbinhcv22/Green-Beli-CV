using GRBESystem.Definitions;
using Localization.Nation;
using Network.Service;
using Network.Service.Implement;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Battle
{
    public class BattlePlayerNationImage : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        [SerializeField] private Image image;
        [SerializeField] private NationConfig nationConfig;

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var startGameResponse = StartGameServerService.Response;
            if (startGameResponse.IsError) return;

            var nation = startGameResponse.data.GetPlayerInfo(owner).nation;
            image.sprite = nationConfig.GetNation(nation).art;
        }
    }
}