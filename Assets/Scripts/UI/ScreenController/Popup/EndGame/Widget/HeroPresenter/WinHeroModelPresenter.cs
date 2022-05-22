using GEvent;
using Extensions.Initialization.Request;
using Network.Messages.GetHeroList;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace UI.ScreenController.Popup.EndGame.Widget.HeroPresenter
{
    public class WinHeroModelPresenter : MonoBehaviour
    {
        [SerializeField, Space] private ShowHeroModelRequest showHeroModelRequest;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, () => Invoke(nameof(PresentModel),
                EndGameServerService.DelayConfig.battleResultPopup));
        }

        private void PresentModel()
        {
            var battleMode = EventManager.GetData(EventName.Client.Battle.BattleMode);
            if (battleMode is null || (BattleMode)battleMode is BattleMode.PvP) return;

            var mainHeroId = EventManager.GetData<HeroResponse>(EventName.Client.MainHeroOldable).GetID();
            showHeroModelRequest.heroId = mainHeroId;

            EventManager.EmitEvent(EventName.Model.HideAllModels);
            EventManager.EmitEventData(EventName.Model.ShowHeroModel, showHeroModelRequest);
        }
    }
}