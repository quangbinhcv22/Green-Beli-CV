using System;
using GEvent;
using Network.Messages;
using Network.Service;
using UnityEngine;

namespace GRBESystem.Widgets.SendMessage
{
    public abstract class MessageSender : MonoBehaviour
    {
        private enum MessageEnum
        {
            JoinArenaPvP,
            JoinArenaPvE,
            LeaveArena,
        }

        [SerializeField] private MessageEnum messageToScreen;

        protected void SendMessage()
        {
            switch (messageToScreen)
            {
                case MessageEnum.JoinArenaPvP:
                    SendMessageNoneResponse(EventName.Server.JoinArena);
                    break;
                case MessageEnum.JoinArenaPvE:
                    NetworkService.Instance.services.playPve.SendRequest();
                    break;
                case MessageEnum.LeaveArena:
                    SendMessageNoneResponse(EventName.Server.LeaveArena);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void SendMessageNoneResponse(string messageId, object dataRequest = null)
        {
            Message.Instance().SetId(messageId).SetRequest(dataRequest).SetResponse(null).Send();
        }
    }
}