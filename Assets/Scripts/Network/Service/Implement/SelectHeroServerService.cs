using System;
using GEvent;
using Network.Messages;
using Newtonsoft.Json;
using TigerForge;
using UI.ScreenController.Window.Battle.Mode;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(SelectHeroServerService), menuName = "ScriptableObject/Service/Server/SelectHero")]
    public class SelectHeroServerService : ScriptableObject, IDeserializeResponseMessage<string>
    {
        [NonReorderable] private MessageResponse<string> _messageResponse;
        public MessageResponse<string> MessageResponse => _messageResponse;


        public void SendRequestEmpty()
        {
            SendRequest(Array.Empty<long>());
        }

        public void SendRequest(long[] heroIds, BattleMode battleMode = BattleMode.PvE)
        {
            UIRequest.ShowDelayPanel.SendRequest();
            Message.Instance().SetId(EventName.Server.SelectTeamHero).SetRequest(new SelectHero.SelectHeroRequest()
            {
                heroIds = heroIds, 
                gameMode = battleMode is BattleMode.PvP ? nameof(GameMode.PVP) : nameof(GameMode.PVE),
            }).Send();
        }

        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            UIRequest.HideDelayPanel.SendRequest();
            
            _messageResponse = JsonConvert.DeserializeObject<MessageResponse<string>>(message);
            return _messageResponse;
        }
    }
}