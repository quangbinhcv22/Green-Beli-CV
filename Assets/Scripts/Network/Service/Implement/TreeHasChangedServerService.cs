using System;
using System.Collections.Generic;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UI.Widget.Toast;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/TreeHasChanged", fileName = nameof(TreeHasChangedServerService))]
    public class TreeHasChangedServerService : ScriptableObject, IServerAPI
    {
        private static TreeHasChangedServerService Instance => NetworkApiManager.GetAPI<TreeHasChangedServerService>();

        [NonSerialized] private MessageResponse<TreeHasChanged> _response;
        public static MessageResponse<TreeHasChanged> Response => Instance._response;
        [SerializeField] private GreenBeliToastDataSet toastDataSet;

        public string APIName => EventName.Server.TreeHasChanged;

        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<TreeHasChanged>>(message);

            if (_response.IsError) return;

            var checkEventStage = _response.data.info.Equals("OPEN_TREE_SUCCESS");
            if(checkEventStage) EventManager.StartListening(EventName.Server.GetListTree,OnHaveNewTree);
            else EventManager.EmitEventData(EventName.ScreenEvent.ShowToastPanel, toastDataSet.treeHasChanged);
            GetListTreeServerService.SendRequest();
        }

        private void OnHaveNewTree()
        {
            EventManager.EmitEvent(EventName.Server.HaveNewTree);
            EventManager.StopListening(EventName.Server.GetListTree,OnHaveNewTree);
        }
    }
}

[Serializable]
public class TreeHasChanged
{
    [JsonProperty("treeId")]
    public int id;
    [JsonProperty("event")]
    public string info;
}