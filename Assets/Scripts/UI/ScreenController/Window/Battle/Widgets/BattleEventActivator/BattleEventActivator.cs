using GEvent;
using TigerForge;
using UnityEngine;


namespace UI.ScreenController.Window.Battle.Widgets.BattleEventActivator
{
    public class BattleEventActivator : MonoBehaviour
    {
        [SerializeField] private bool enableWhenStartGame;
        [SerializeField] private bool enableWhenEndGame;

        private void Awake()
        {
            EventManager.StartListening(EventName.Server.StartGame, () => SetActive(enableWhenStartGame));
            EventManager.StartListening(EventName.Server.EndGame, () => SetActive(enableWhenEndGame));
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}
