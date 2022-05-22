using System;
using System.Collections.Generic;
using Log;
using Network.Messages;
using Newtonsoft.Json;
using Pattern;
using TigerForge;
using UnityEngine;

namespace GNetwork
{
    [DefaultExecutionOrder(-1)]
    public class NetworkApiManager : Singleton<NetworkApiManager>
    {
        private static Dictionary<Type, ScriptableObject> _servicesByTypeDic;
        private static Dictionary<string, IServerAPI> _serviceByIdDic;

        [SerializeField] private NetworkAPIConfig config;


        private void Awake()
        {
            SetConfig();

            void SetConfig()
            {
                _servicesByTypeDic = config.ToByTypeDictionary();
                _serviceByIdDic = config.ToByIdDictionary();
            }
        }

        public static T GetAPI<T>() where T : ScriptableObject
        {
            var type = typeof(T);
            return _servicesByTypeDic.ContainsKey(type) ? (T) _servicesByTypeDic[type] : null;
        }

        public static void OnResponse(string message)
        {
            GLogger.LogLog($"Received: <color=yellow>{message}</color>");

            var apiID = JsonConvert.DeserializeObject<Id>(message).id;

            if (_serviceByIdDic.ContainsKey(apiID))
            {
                _serviceByIdDic[apiID].OnResponse(message);
                EventManager.EmitEvent(apiID);
            }
            else
            {
                GLogger.LogLog($"Not implement <color=pink>{apiID}</color> in new Server API");
            }
        }
    }
}