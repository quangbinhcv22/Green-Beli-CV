using System;
using GEvent;
using Network.Messages;
using Network.Messages.SelectCard;
using TigerForge;
using UnityEngine;

namespace Network.Service
{
    public struct NetworkServiceSendMessage
    {
        private static void SendMessage(string messageId, Action<object> onResponse, object dataRequest = null)
        {
            Message.Instance().SetId(messageId).SetRequest(dataRequest).SetResponse(onResponse).Send();
        }

        public void SelectHero(SelectHero.SelectHeroRequest dataRequest, Action<string> onResponse)
        {
            SendMessage(EventName.Server.SelectTeamHero,
                onResponse: responseObject => onResponse?.Invoke((string)responseObject), dataRequest);
        }

        public void JoinArena(Action<string> onResponse = null)
        {
            SendMessage(EventName.Server.JoinArena,
                onResponse: responseObject => onResponse?.Invoke((string)responseObject));
        }

        public void GetHeroList()
        {
            SendMessage(EventName.Server.GetListHero, NothingCallback);
        }
        

        private void NothingCallback(object obj)
        {
        }
    }
}