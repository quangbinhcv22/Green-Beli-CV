using System;
using GEvent;
using Network.Messages;
using Network.Messages.NftTree;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/HarvestTree", fileName = nameof(HarvestTreeFruitServerService))]
    public class HarvestTreeFruitServerService : ScriptableObject, IServerAPI
    {
        private static HarvestTreeFruitServerService Instance =>
            NetworkApiManager.GetAPI<HarvestTreeFruitServerService>();

        [NonSerialized] private MessageResponse<HarvestTreeFruitRespone> _response;

        public static MessageResponse<HarvestTreeFruitRespone> Response => Instance._response;

        public string APIName => EventName.Server.HarvestTreeFruit;

        public static void SendRequest(HarvestTreeFruitRequest data)
        {
            Message.Instance().SetId(EventName.Server.HarvestTreeFruit).SetRequest(data).Send();
        }

        public void OnResponse(string message)
        {
            _response = JsonUtility.FromJson<MessageResponse<HarvestTreeFruitRespone>>(message);
        }
    }

    [Serializable]
    public class HarvestTreeFruitRequest
    {
        public int treeId;
    }
}