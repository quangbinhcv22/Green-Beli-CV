using System.Collections.Generic;
using GEvent;
using GRBESystem.UI.Screens.Windows.Leaderboard;
using Network.Messages;
using Network.Service;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.OpenPvpChest
{
    [RequireComponent(typeof(Button))]
    public class OpenPvpChestButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private int numberKey;
        [SerializeField] private ScreenRequest screenRequest;
        [SerializeField] [Space] private Color normalColor;
        [SerializeField] private Color unEnableColor;


        private void Awake()
        {
            button ??= GetComponent<Button>();
            button.onClick.AddListener(OpenPvpChestRequest);
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Client.EventPvpKeyUpdate, UpdateView);
            EventManager.StartListening(EventName.Server.OpenPvpBoxRewardEarnKey, OnOpenScreen);
        }
         
        private void OnDisable()
        {
            EventManager.StopListening(EventName.Client.EventPvpKeyUpdate, UpdateView);
            EventManager.StopListening(EventName.Server.OpenPvpBoxRewardEarnKey, OnOpenScreen);
        }

        private void UpdateView()
        {
            var response = NetworkService.Instance.services.login.MessageResponse;
            if (NetworkService.Instance.IsNotLogged() || response.IsError) return;

            // button.enabled = response.data.numberPVPKey >= numberKey;
            // button.image.color = response.data.numberPVPKey >= numberKey ? normalColor : unEnableColor;
        }

        private void OnOpenScreen()
        {
            var response =
                EventManager.GetData<MessageResponse<List<OpenPvpChestResponse>>>(EventName.Server
                    .OpenPvpBoxRewardEarnKey);
            if (response.IsError is false && response.data.Count == numberKey)
                EventManager.EmitEventData(EventName.UI.RequestScreen(), screenRequest);
        }

        private void OpenPvpChestRequest()
        {
            NetworkService.Instance.services.openPvpBox.SendRequest(numberKey);
        }
    }
}
