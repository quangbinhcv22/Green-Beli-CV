using System;
using System.Collections.Generic;
using GEvent;
using Manager.Game;
using Network.Controller;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(PlayPveServerService), menuName = "ScriptableObject/Service/Server/PlayPve")]
    public class PlayPveServerService : ScriptableObject, IDeserializeResponseMessage<string>
    {
        [NonSerialized] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;


        [SerializeField] private string playerPveChooseHeroError;


        public void SendRequest()
        {
            var selectedHeroes = EventManager.GetData<List<long>>(EventName.PlayerEvent.BattleHeroes);
            if (GameManager.Instance.selectHeroConfig.HaveMainHero(selectedHeroes) is false)
            {
                WebSocketController.Instance.SendFakeReceivedMessage(playerPveChooseHeroError);
                return;
            }

            UIRequest.ShowDelayPanel.SendRequest();
            Message.Instance().SetId(EventName.Server.PlayPve).Send();
        }

        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);

            if (_response.IsError) UIRequest.HideDelayPanel.SendRequest();

            return Response;
        }
    }
}