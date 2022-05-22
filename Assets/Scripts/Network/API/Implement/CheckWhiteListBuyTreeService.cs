using System;
using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Network.Service;
using Newtonsoft.Json;
using TigerForge;
using UnityEngine.Networking;

[Serializable]
public static class CheckWhiteListBuyTreeService
{
    public static CheckWhiteListBuyTreeResponse Response;


    public static async void SendRequest(bool isCheckWhitelist = false, string address = null)
    {
        address ??= NetworkService.Instance.services.login.LoginResponse.id;
        EventManager.EmitEventData(EventName.Select.IsCheckWhiteList, isCheckWhitelist);
        OnResponse(await HaveInWhiteList(address));
    }
    
    private static void OnResponse(string message)
    {
        GLogger.LogLog($"Response: <color=yellow>{message}</color>");

        Response = JsonConvert.DeserializeObject<CheckWhiteListBuyTreeResponse>(message);

        var nullableCheck = EventManager.GetData(EventName.Select.IsCheckWhiteList);

        if (nullableCheck is bool isCheckWhiteList)
        {
            switch (isCheckWhiteList)
            {
                case false:
                    EventManager.EmitEvent(EventName.Server.SetEventTime);
                    break;
                default:
                    EventManager.EmitEvent(EventName.Server.CheckWhiteListBuyTree);
                    break;
            }
        }
    }

    private static async UniTask<string> HaveInWhiteList(string address)
    {
        var url = string.Format(BlockChainConfig.requestOpenTreeInfo, BlockChainConfig.serverUrl, address);

        GLogger.LogLog($"Send: <color=#00B0F0>{url}</color>");
        return (await UnityWebRequest.Get(url).SendWebRequest().ToUniTask()).downloadHandler.text;
    }
}

[Serializable]
public class CheckWhiteListBuyTreeResponse
{
    [JsonProperty("can_open")] public bool canOpen;
    [JsonProperty("info")] public string info;
    [JsonProperty("time")] public SetTime time;
}

[Serializable]
public class SetTime
{
    public string start;
    public string whitelist;
    public string end;
}