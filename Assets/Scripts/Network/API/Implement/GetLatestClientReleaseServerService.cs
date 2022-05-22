using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(menuName = "NetworkAPI/GetLatestClientRelease", fileName = nameof(GetLatestClientReleaseServerService))]
    public class GetLatestClientReleaseServerService : ScriptableObject, IServerAPI
    {
        public enum Platform
        {
            Web = 0,
            Android = 1,
            IOS = 2,
        }

        private static GetLatestClientReleaseServerService Instance => NetworkApiManager.GetAPI<GetLatestClientReleaseServerService>();

        [NonSerialized] private MessageResponse<List<LatestPlatformReleaseResponse>> _response;
        public static MessageResponse<List<LatestPlatformReleaseResponse>> Response => Instance._response;


        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetLatestClientRelease).Send();
        }
        
        public string APIName => EventName.Server.GetLatestClientRelease;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<List<LatestPlatformReleaseResponse>>>(message);
        }


        public static string GetLatestPlatformOnThisDeviceRelease()
        {
            const string somethingWentWrong = "Something went wrong";
            if (Response.IsError) return somethingWentWrong;

            var queryPlatformRelease = Response.data.Find(platformRelease => platformRelease.platform == PlatformThisDevice());
            return queryPlatformRelease is null ? somethingWentWrong : queryPlatformRelease.version;

            
            static string PlatformThisDevice()
            {
#if UNITY_ANDROID
                return LatestPlatformReleaseResponse.Version.Android;
#elif UNITY_IOS
                return LatestPlatformReleaseResponse.Version.IOS;
#else
                return LatestPlatformReleaseResponse.Version.Web;
#endif
            }
        }

        public static string GetUrl(Platform platform)
        {
            return Response.IsError
                ? null
                : Response.data.Find(platformRelease => platformRelease.platform == platform.ToString().ToUpper()).url;
        }
    }

    [Serializable]
    public class LatestPlatformReleaseResponse
    {
        public static class Version
        {
            public const string Web = "WEB";
            public const string Android = "ANDROID";
            public const string IOS = "IOS";
        }

        public string time;
        public string version;
        public string platform;
        public string url;
    }
}