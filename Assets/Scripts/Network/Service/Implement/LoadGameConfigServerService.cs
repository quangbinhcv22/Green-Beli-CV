using System;
using System.IO;
using System.Linq;
using GEvent;
using Network.Messages;
using Network.Messages.LoadGame;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(LoadGameConfigServerService),
        menuName = "ScriptableObject/Service/Server/LoadGameConfig")]
    public class LoadGameConfigServerService : ScriptableObject, IDeserializeResponseMessage<LoadGameConfigResponse>
    {
#if UNITY_EDITOR
        [SerializeField] private TextDebugConfig debugConfig;
#endif

        [SerializeField] private MessageResponse<LoadGameConfigResponse> _response;
        public MessageResponse<LoadGameConfigResponse> Response => _response;


        public void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.LoadGameConfig).Send();
        }

        public MessageResponse<LoadGameConfigResponse> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<LoadGameConfigResponse>>(message);

#if UNITY_EDITOR
            // debugConfig.SetContent(message).Write();
#endif

            return _response;
        }
        
        public int GetPvpTicketRequire()
        {
            return _response.IsError
                ? int.MaxValue
                : _response.data.pvp.pvpticket_require_pvpkey_reward[default].pvp_ticket_require;
        }


        [Obsolete] public LoadGameConfigResponse ResponseData => _response.data;

        [Obsolete]
        public long GetTokenRewardAtHeroStar(int star)
        {
            return star switch
            {
                1 => long.Parse(ResponseData.pve.GetRewardGFruit().star1),
                2 => long.Parse(ResponseData.pve.GetRewardGFruit().star2),
                3 => long.Parse(ResponseData.pve.GetRewardGFruit().star3),
                4 => long.Parse(ResponseData.pve.GetRewardGFruit().star4),
                5 => long.Parse(ResponseData.pve.GetRewardGFruit().star5),
                6 => long.Parse(ResponseData.pve.GetRewardGFruit().star6),
                _ => default
            };
                
        }
    }

    public static class DebugFileTxt
    {
        public static void WriteText(TextDebugConfig debugConfig)
        {
            var path = $"{Application.dataPath}/{debugConfig.path}";
            var createText = debugConfig.content + Environment.NewLine;
            
            if (File.Exists(path) is false) File.CreateText(path);
            File.WriteAllText(path, createText);
        }
    }

    [Serializable]
    public class TextDebugConfig
    {
        public string path;
        [NonSerialized] public string content;

        public TextDebugConfig SetContent(string newContent)
        {
            content = newContent;
            return this;
        }

        public void Write()
        {
            DebugFileTxt.WriteText(this);
        }
    }
}