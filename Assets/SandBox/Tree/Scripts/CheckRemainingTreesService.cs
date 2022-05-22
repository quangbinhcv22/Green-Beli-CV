using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GEvent;
using Log;
using Newtonsoft.Json;
using TigerForge;
using UnityEngine;
using UnityEngine.Networking;

public static class CheckRemainingTreesService
{
    public static CheckRemainingTreesResponse Response;
    
    public static async void SendRequest()
    {
        OnResponse(await HaveInWhiteList());
    }
    
    private static void OnResponse(string message)
    {
        GLogger.LogLog($"Response: <color=yellow>{message}</color>");

        Response = JsonConvert.DeserializeObject<CheckRemainingTreesResponse>(message);

        if (Response is null) SendRequest();
        else
        {
            var remainingTree = Response.remainingTree;
            EventManager.EmitEventData(EventName.Server.RemainingTree,remainingTree);
        }
    }
    
    private static async UniTask<string> HaveInWhiteList()
    {
        var url = string.Format(BlockChainConfig.requestRemainingTree, BlockChainConfig.serverUrl);
        return (await UnityWebRequest.Get(url).SendWebRequest().ToUniTask()).downloadHandler.text;
    }
}

[Serializable]
public class CheckRemainingTreesResponse
{
    [JsonProperty("remainingTree")] public int remainingTree;
}