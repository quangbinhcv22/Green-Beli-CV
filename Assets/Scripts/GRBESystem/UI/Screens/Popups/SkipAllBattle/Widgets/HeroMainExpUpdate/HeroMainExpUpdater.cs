using System.Linq;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.HeroMainExpUpdate
{
    public class HeroMainExpUpdater : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.SkipAllGame, UpdateHeroExp);
        }

        private void UpdateHeroExp()
        {
            if (IsSkipAllGameFail() || NetworkService.Instance.services.getHeroList.HeroResponses == null) return;

            var mainHero = NetworkService.Instance.services.getHeroList.HeroResponses.GetMainHero()
                .AddFakeExp(GetSkipAllGameTotalExp());
            NetworkService.Instance.services.getHeroList.ModifyHero(mainHero);
        }

        private int GetSkipAllGameTotalExp()
        {
            return IsSkipAllGameFail() ? default : SkipAllGameServerService.Data.Sum(response => response.rewardExp);
        }

        private bool IsSkipAllGameFail()
        {
            var skipAllResponse = SkipAllGameServerService.Response;
            return skipAllResponse.IsError || skipAllResponse.data == null;
        }
    }
}