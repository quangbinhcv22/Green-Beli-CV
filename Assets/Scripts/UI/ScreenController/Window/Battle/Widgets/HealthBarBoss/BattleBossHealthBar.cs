using System.Collections;
using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UI.Widget;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.HealthBarBoss
{
    public class BattleBossHealthBar : MonoBehaviour
    {
        [SerializeField, Space] private ProcessBar healthBar;
        [SerializeField] private TMP_Text text;

        private float _maxHealth;
        private float _delayUpdateView;


        private void Awake()
        {
            _delayUpdateView =  AttackBossServerService.delayResponseCallbackConfig.healthBarChange;
            EventManager.StartListening(EventName.Server.AttackBoss, () => StartCoroutine(OnAttackBoss()));
        }

        private void OnEnable()
        {
            OnStartGame();
        }

        private void OnStartGame()
        {
            if (StartGameServerService.Response.IsError) return;

            _maxHealth = StartGameServerService.Response.data.boss.healthInit;
            UpdateView(_maxHealth);
        }

        private IEnumerator OnAttackBoss()
        {
            yield return new WaitForSeconds(_delayUpdateView);
            UpdateView(AttackBossServerService.Response.data.bossHealth);
        }

        private void UpdateView(float health)
        {
            healthBar.UpdateView(health, _maxHealth);
            text.SetText($"{health:N0}/{_maxHealth:N0}");
        }
    }
}