using System.Collections.Generic;
using GEvent;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace UI.ScreenController.Window.Battle
{
    public class BattleWindowHandler : MonoBehaviour
    {
        [SerializeField] private List<GameObject> gameObjects;
        [SerializeField] private List<UIRequest> uiRequests;
        [SerializeField] private float time;
        [SerializeField] private bool isActive = true;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, HideUIs);
        }
        
        private void OnEnable()
        {
            gameObjects.ForEach(item => item.gameObject.SetActive(isActive is false));
            Invoke(nameof(SetActive), time);
        }

        private void OnDisable()
        {
            HideUIs();
        }

        private void HideUIs()
        {
            uiRequests.ForEach(item =>
            {
                if (item.action != UIAction.Open) return;
                
                item.action = UIAction.Close;
                item.SendRequest();
                item.action = UIAction.Open;
            });
        }

        private void SetActive()
        {
            uiRequests.SendRequest();
            gameObjects.ForEach(item => item.gameObject.SetActive(isActive));
        }
    }
}