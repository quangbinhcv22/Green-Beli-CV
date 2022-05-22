using GEvent;
using Extensions.Text;
using Network.Messages.AttackBoss;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.FieldTextViewer.AttackBossDamage
{
    public class BattleAttackBossDamageText : MonoBehaviour
    {
        [SerializeField, Space] private VersatileText damageText;
        [SerializeField] private TextFloatingConfig textFloatingConfig;

        [System.Serializable]
        public struct TextFloatingConfig
        {
            public Vector3 distance;
            public float duration;
        }

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.AttackBoss, () => Invoke(nameof(OnAttackBoss),
                AttackBossServerService.delayResponseCallbackConfig.showDamageText));

            gameObject.SetActive(false);
        }

        private void OnAttackBoss()
        {
            var attackBossResponse = AttackBossServerService.Response.data;
            if (attackBossResponse.IsRoundDraw()) return;

            damageText.FloatingText(textFloatingConfig.distance, textFloatingConfig.duration,
                attackBossResponse.attackDamage, attackBossResponse.isCritDamage);
        }
    }
}