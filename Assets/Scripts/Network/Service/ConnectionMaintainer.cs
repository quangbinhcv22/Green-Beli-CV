using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Network.Messages;
using TigerForge;
using UnityEngine;

namespace Network.Service
{
    public class ConnectionMaintainer : MonoBehaviour
    {
        private const int REPEAT_INTERVAL = 15;
        
        private bool _ping;

        private float _totalSeconds;
        
        private void Start()
        {
            _totalSeconds = 0;
            
            EventManager.StartListening(EventName.Client.LoadEssentialData, TriggerPing);
            EventManager.StartListening(EventName.Server.Disconnect, DeTriggerPing);
            
            TimeManager.Instance.AddEvent((int seconds) =>
            {
                if (_ping == false)
                    return;
                
                _totalSeconds++;
                
                if (_totalSeconds >= REPEAT_INTERVAL)
                {
                    _totalSeconds = 0;
                    SendEmptyMessageToServer();
                }
            });
        }

        private void TriggerPing()
        {
            _ping = true;
        }
        
        private void DeTriggerPing()
        {
            _ping = false;
        }

        private void SendEmptyMessageToServer()
        {
            GLogger.LogLog("Call SendEmptyMessageToServer: " + DateTime.UtcNow);
            Message.Instance().SetId(EventName.Server.Ping).SetRequest(null).SetResponse(null).Send();
        }
    }
}