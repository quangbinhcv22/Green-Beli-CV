using GEvent;
using Network.Service.Implement;
using Service.Server.EndGame;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Popup.EndGame.Widget.SoundEffect
{
    public class BattleResultSoundEffectEmitter : MonoBehaviour
    {
        [SerializeField] private BattleResultSoundEffectSet soundSet;
        

        private void Awake() => EventManager.StartListening(EventName.Server.EndGame,
            () => Invoke(nameof(OneEndGame), EndGameServerService.DelayConfig.battleResultPopup));

        private void OneEndGame() => Play(EndGameServerService.GetClientData().GetResultBattle());

        private void Play(EndGameClientData.ResultBattle result) =>
            EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_ACTION, soundSet.GetSound(result));

    }
}