using System.Collections;
using GEvent;
using GRBESystem.Definitions;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.DamageViewer
{
    public class BattlePlayerDamageText : MonoBehaviour
    {
        [SerializeField] private Owner owner;
        [SerializeField] private TMP_Text damageText;

        private float _delayUpdateView;

        private void Awake()
        {
            _delayUpdateView = AttackBossServerService.delayResponseCallbackConfig.showDamageText;
            EventManager.StartListening(EventName.Server.AttackBoss, () => StartCoroutine(UpdateViewHaveDelay()));
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            var totalDamage = AttackBossServerService.GetTotalDamage(owner);
            damageText.SetText($"{totalDamage:N0}");
        }

        private IEnumerator UpdateViewHaveDelay()
        {
            yield return new WaitForSeconds(_delayUpdateView);
            UpdateView();
        }
    }
}