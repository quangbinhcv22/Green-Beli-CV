using System.Collections.Generic;
using System.Linq;
using Config.Other;
using GEvent;
using Manager.Game;
using Network.Service;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero.SelectHero
{
    public class BattleHeroesSelectMessageSender : MonoBehaviour
    {
        private List<long> _lastSendableSelectedHeroes = new List<long>();

        private static SelectHeroConfig SelectHeroConfig => GameManager.Instance.selectHeroConfig;

        private void OnEnable()
        {
            EventManager.StartListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.PlayerEvent.BattleHeroes, OnSelectBattleHeroes);
        }

        private void OnSelectBattleHeroes()
        {
            var selectedEmptiableHeroIds = GetSelectedEmptiableHeroIds();
            if (SelectHeroConfig.IsValidSelectedHeroes(selectedEmptiableHeroIds) == false) return;

            var sendableHeroIds = selectedEmptiableHeroIds.Where(SelectHeroConfig.IsValidHeroID).ToList();
            
            if (IsModifiedComparedLastSend(sendableHeroIds) == false) return;

            var battleModeData = EventManager.GetData(EventName.Client.Battle.BattleMode);
            var mode = battleModeData is null ? BattleMode.PvE :
                (BattleMode) battleModeData is BattleMode.PvP ? BattleMode.PvP : BattleMode.PvE;
            
            _lastSendableSelectedHeroes = sendableHeroIds;
            NetworkService.Instance.services.selectHero.SendRequest(sendableHeroIds.ToArray(), mode);
        }

        private bool IsModifiedComparedLastSend(List<long> newHeroIds)
        {
            var haveModify = newHeroIds.Count != _lastSendableSelectedHeroes.Count;

            if (haveModify == false)
            {
                for (int i = 0; i < Mathf.Min(newHeroIds.Count, _lastSendableSelectedHeroes.Count); i++)
                {
                    haveModify = newHeroIds[i] != _lastSendableSelectedHeroes[i];
                    if (haveModify) break;
                }
            }

            return haveModify;
        }

        private static List<long> GetSelectedEmptiableHeroIds()
        {
            var nullableHeroIds = EventManager.GetData(EventName.PlayerEvent.BattleHeroes);
            return nullableHeroIds is null ? new List<long>() : (List<long>)nullableHeroIds;
        }
    }
}