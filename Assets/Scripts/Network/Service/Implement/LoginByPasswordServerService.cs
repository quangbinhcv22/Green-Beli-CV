using System;
using GEvent;
using GNetwork;
using Manager.Inventory;
using Network.Messages;
using Newtonsoft.Json;
using QB.Security;
using UIFlow;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(LoginByPasswordServerService), menuName = "NetworkAPI/LoginByPassword")]
    public class LoginByPasswordServerService : ScriptableObject, IServerAPI
    {
        private static LoginByPasswordServerService Instance => NetworkApiManager.GetAPI<LoginByPasswordServerService>();
        public static MessageResponse<LoginResponse> Response => Instance._response;
        public static LoginServerService.LoginMode LoginMode => Instance._loginMode;
        
        
        [NonSerialized] private LoginServerService.LoginMode _loginMode;
        [NonSerialized] private MessageResponse<LoginResponse> _response;
        
        
        public static void SendRequest(ILoginByHashPasswordRequest loginByPasswordRequest)
        {
            Message.Instance().SetId(EventName.Server.LoginByPassword).SetRequest(loginByPasswordRequest.HashPassword()).Send();
        }

        public string APIName => EventName.Server.LoginByPassword;
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<LoginResponse>>(message);

            if (_response.IsError is false)
            {
                NetworkService.playerInfo.address = _response.data.id;

                _loginMode = _response.data.role.ToUpper() switch
                {
                    "METAMASK" => LoginServerService.LoginMode.MetaMask,
                    "MASTER" => LoginServerService.LoginMode.Master,
                    "SLAVE" => LoginServerService.LoginMode.Slave,
                    _ => _loginMode
                };

                NetworkService.playerInfo.inventory.SetMoney(MoneyType.PvpTicket,
                    _response.data.numberPVPTicket);
                
                NetworkService.playerInfo.inventory.SetMoney(MoneyType.BeLi, 
                    _response.data.belToken);
            }

            UIRequest.HideDelayPanel.SendRequest();
        }
    }

    public interface ILoginByHashPasswordRequest
    {
        public ILoginByHashPasswordRequest HashPassword();
    }

    [System.Serializable]
    public class LoginByHashMasterPasswordRequest : ILoginByHashPasswordRequest
    {
        public string address;
        public string masterPassword;

        public ILoginByHashPasswordRequest HashPassword()
        {
            masterPassword = HashUtility.MD5Hash(masterPassword);
            return this;
        }
    }
    
    [System.Serializable]
    public class LoginByHashSlavePasswordRequest : ILoginByHashPasswordRequest
    {
        public string address;
        public string slavePassword;

        public ILoginByHashPasswordRequest HashPassword()
        {
            slavePassword = HashUtility.MD5Hash(slavePassword);
            return this;
        }
    }
}