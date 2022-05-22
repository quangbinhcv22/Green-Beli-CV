using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Network.Service;
using Network.Service.Implement;
using SandBox.DataInformation;
using SandBox.Tree.InfomartionPopup;
using TigerForge;
using UnityEngine;

namespace UIFlow
{
    [CreateAssetMenu(menuName = "UIFlow/FlowConfig", fileName = nameof(UIFlowConfig))]
    public class UIFlowConfig : ScriptableObject
    {
        [Serializable]
        public class Requests
        {
            public List<UIRequest> onAwake;
            public List<UIRequest> onConnect;
            public List<UIRequest> onDisconnect;
            public List<UIRequest> onLoggedIn;
            public List<UIRequest> onOutOfVersion;
            public List<UIRequest> onEssentialDataLoaded;
            public List<UIRequest> onConvertKeyPvp;
            public List<UIRequest> onBreedingSuccess;
            public List<UIRequest> onFusionSuccess;
            public List<UIRequest> onWinLottery;
            public List<UIRequest> onShowToast;
            public List<UIRequest> onBuyTreeSuccess;
            public List<UIRequest> onReceivedFormMysteryChest;
        }

        [Serializable]
        public class InfoDataPreset
        {
            public UIRequest openInfoPopup;
            public InfoPopupPreset loginSameAddress;
            public InfoPopupPreset notSelectHero;
            public InfoPopupPreset notEnoughEnergy;
            public InfoPopupPreset empty;
        }

        public List<UIFrame2> framePrefabs;
        public Requests requests;
        public InfoDataPreset infoDataPreset;

        public void RegisterEvents()
        {
            EventManager.StartListening(EventName.Server.LoginByMetamask, OnLogin);
            EventManager.StartListening(EventName.Server.LoginByMetamask, OnLogin);
            EventManager.StartListening(EventName.Server.GetLatestClientRelease, OnCheckVersion);
            EventManager.StartListening(EventName.Server.HaveNewTree, OnBuyTreeSuccess);
            EventManager.StartListening(EventName.Server.OpenMysteryChest, OnOpenMysteryChest);


            var events = new Dictionary<string, List<UIRequest>>
            {
                {EventName.Server.BreedingSuccess, requests.onBreedingSuccess},
                {EventName.Server.FusionSuccess, requests.onFusionSuccess},
                {EventName.Server.Connect, requests.onConnect},
                {EventName.Server.Disconnect, requests.onDisconnect},
                {EventName.Server.Stopped, requests.onDisconnect},
                {EventName.Server.WinLottery, requests.onWinLottery},
                {EventName.ScreenEvent.ShowToastPanel, requests.onShowToast},

                {EventName.Client.LoadEssentialData, requests.onEssentialDataLoaded},
                {EventName.Server.ConvertPvpKeyToReward, requests.onConvertKeyPvp},
            };

            foreach (var @event in events)
            {
                EventManager.StartListening(@event.Key, @event.Value.SendRequest);
            }
        }

        private void OnLogin()
        {
            var response = NetworkService.Instance.services.login.MessageResponse;
            if (response.IsError)
            {
                if (response.error.Contains("Invalid Advanced Password"))
                    return;

                InfoPopupData2 popupData;

                if (response.error.Contains("Someone is login with same address"))
                {
                    popupData = infoDataPreset.loginSameAddress.data;
                }
                else if (response.error.Contains("Invalid"))
                {
                    return;
                }
                else
                {
                    popupData = infoDataPreset.empty.data;
                }

                popupData.content = response.error;
                infoDataPreset.openInfoPopup.data = popupData;
                infoDataPreset.openInfoPopup.SendRequest();
            }
            else
            {
                requests.onLoggedIn.SendRequest();
            }
        }

        private void OnCheckVersion()
        {
            if (GetLatestClientReleaseServerService.Response.IsError) return;

            var currentVersion = Application.version;
            var latestVersion = GetLatestClientReleaseServerService.GetLatestPlatformOnThisDeviceRelease();

            if (currentVersion == latestVersion) return;

            requests.onOutOfVersion.SendRequest();
        }

        private void OnBuyTreeSuccess()
        {
            if (TreeHasChangedServerService.Response.IsError) return;
            var checkEventStage = TreeHasChangedServerService.Response.data.info.Equals("OPEN_TREE_SUCCESS");
            if(checkEventStage) requests.onBuyTreeSuccess.SendRequest();
        }

        private void OnOpenMysteryChest()
        {
            if (OpenMysteryChestServerService.Response.IsError) return;
            requests.onReceivedFormMysteryChest.SendRequest();
            NetworkService.Instance.services.getRewardHistoryAll.SendRequest();
        }
    }

    public static class UIRequestExtension
    {
        public static void SendRequest(this List<UIRequest> requests)
        {
            requests.ForEach(request => request.SendRequest());
        }
    }
}