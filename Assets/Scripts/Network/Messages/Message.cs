using System;
using Log;
using Network.Controller;
using TigerForge;
using UnityEngine;

namespace Network.Messages
{
    public class Message
    {
        private string _id;

        private RequestData _requestData;

        private Action<object> _callback;

        public Message SetId(string id)
        {
            this._id = id;

            this._requestData.id = id;

            return this;
        }

        public Message SetRequest(object data)
        {
            this._requestData.data = data;

            return this;
        }

        public Message SetResponse(Action<object> callback)
        {
            this._callback = callback;

            EventManager.StartListening(this._id, CallbackResponse);

            return this;
        }

        public void Send()
        {
            if (this._requestData.Equals(null))
            {
                throw new ArgumentNullException();
            }

            var message = _requestData.ConvertToJson();

            GLogger.LogLog($"Send: <color=#00B0F0>{message}</color>");
            WebSocketController.Instance.Send(message);
        }

        private void CallbackResponse()
        {
            var data = EventManager.GetData(this._id);
            this._callback?.Invoke(data);

            EventManager.StopListening(this._id, CallbackResponse);
        }

        public static Message Instance()
        {
            return new Message();
        }
    }
}