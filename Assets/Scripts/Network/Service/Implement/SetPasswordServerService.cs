using System;
using GEvent;
using GRBEGame.UI.Screen.LoginForm;
using Network.Messages;
using Newtonsoft.Json;
using QB.Security;
using UnityEngine;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(SetPasswordServerService), menuName = "ScriptableObject/Service/Server/SetPassword")]
    public class SetPasswordServerService : ScriptableObject, IDeserializeResponseMessage<string>
    {
        [NonSerialized] private MessageResponse<string> _response;
        public MessageResponse<string> Response => _response;

        private AccountRole _accountRole;
        
        
        public void SendRequest(IBindingPasswordRequest bindingPasswordRequest)
        {
            _accountRole = bindingPasswordRequest.GetAccountRole();
            Message.Instance().SetId(EventName.Server.SetPassword).SetRequest(bindingPasswordRequest.HashPassword()).Send();
        }

        public MessageResponse<string> DeserializeResponseMessage(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<string>>(message);
            
            if (_response.IsError is false && NetworkService.Instance.IsNotLogged() is false)
            {
                switch (_accountRole)
                {
                    case AccountRole.Master:
                        NetworkService.Instance.services.login.LoginResponse.hasMasterPassword = true;
                        break;
                    case AccountRole.Slave:
                        NetworkService.Instance.services.login.LoginResponse.hasSlavePassword = true;
                        break;
                }
            }
            
            return _response;
        }
    }

    
    public interface IBindingPasswordRequest
    {
        public IBindingPasswordRequest HashPassword();
        public AccountRole GetAccountRole();
    }

    [Serializable]
    public class BindingMasterPasswordRequest : IBindingPasswordRequest
    {
        public string masterPassword;
        
        public IBindingPasswordRequest HashPassword()
        {
            masterPassword = HashUtility.MD5Hash(masterPassword);
            return this;
        }

        public AccountRole GetAccountRole()
        {
            return AccountRole.Master;
        }
    }
    
    [Serializable]
    public class BindingSlavePasswordRequest : IBindingPasswordRequest
    {
        public string slavePassword;
        
        public IBindingPasswordRequest HashPassword()
        {
            slavePassword = HashUtility.MD5Hash(slavePassword);
            return this;
        }
        
        public AccountRole GetAccountRole()
        {
            return AccountRole.Slave;
        }
    }
}