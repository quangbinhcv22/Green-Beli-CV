using System;
using GEvent;
using Manager.Inventory;
using Network.Messages;
using Newtonsoft.Json;
// using Sirenix.OdinInspector;
using TigerForge;
using UIFlow;
using UnityEngine;
using Utils;

namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(LoginServerService), menuName = "ScriptableObject/Service/Server/Login")]
    public class LoginServerService : ScriptableObject, IDeserializeResponseMessage<LoginResponse>
    {
        public LoginResponse LoginResponse => _messageResponse.data;
        public bool IsLoggedIn => IsNotLoggedIn == false;

        public bool IsNotLoggedIn => _messageResponse.IsError || (string.IsNullOrEmpty(_messageResponse.data.id) &&
                                                                  string.IsNullOrEmpty(_messageResponse.data.username));

        [NonSerialized] public LoginMode loginMode;


        [NonSerialized] private MessageResponse<LoginResponse> _messageResponse;
        // [ShowInInspector]
        public MessageResponse<LoginResponse> MessageResponse => _messageResponse;

        public bool IsBindingAccount => string.IsNullOrEmpty(_messageResponse.data.username) is false;

        public void SetUserName(string userName)
        {
            _messageResponse.data.username = userName;
        }

        public void SetHasMasterPassword(bool isHasPassword)
        {
            _messageResponse.data.hasMasterPassword = isHasPassword;
        }

        public void SetHasSlavePassword(bool isHasPassword)
        {
            _messageResponse.data.hasSlavePassword = isHasPassword;
        }

        public void SendRequest(LoginRequest loginRequest)
        {
            UIRequest.ShowDelayPanel.SendRequest();

            Message.Instance().SetId(EventName.Server.LoginByMetamask).SetRequest(loginRequest).SetResponse(null)
                .Send();
        }

        public MessageResponse<LoginResponse> DeserializeResponseMessage(string message)
        {
            _messageResponse = JsonConvert.DeserializeObject<MessageResponse<LoginResponse>>(message);

            if (_messageResponse.IsError is false)
            {
                NetworkService.playerInfo.address = _messageResponse.data.id;

                loginMode = _messageResponse.data.role.ToUpper() switch
                {
                    "METAMASK" => LoginMode.MetaMask,
                    "MASTER" => LoginMode.Master,
                    "SLAVE" => LoginMode.Slave,
                    _ => loginMode
                };

                NetworkService.playerInfo.inventory.SetMoney(MoneyType.PvpTicket,
                    _messageResponse.data.numberPVPTicket);
                
                NetworkService.playerInfo.inventory.SetMoney(MoneyType.BeLi, 
                    _messageResponse.data.belToken);
            }

            UIRequest.HideDelayPanel.SendRequest();

            return _messageResponse;
        }

        public enum LoginMode
        {
            MetaMask = 0,
            Slave = 1,
            Master = 2,
        }

        public void SetNation(string nationCode)
        {
            var setTime = DateTime.UtcNow.ToString(DateTimeUtils.GreenBeliFullDateFormat);

            _messageResponse.data.nation = nationCode;
            _messageResponse.data.updatedNationTime = setTime;
        }

        private const int LimitReward = 5;

        public void SubNumberReceivedFreeMaterial()
        {
            if (_messageResponse.data.numberLimitReceivedFreeMaterial >= LimitReward) return;
            _messageResponse.data.numberLimitReceivedFreeMaterial++;
        }

        public int GetNumberReceivedFreeMaterial()
        {
            var countReward = _messageResponse.IsError ? default : _messageResponse.data.numberLimitReceivedFreeMaterial;
            return LimitReward - countReward;
        }

        public void AddPvpTicket(int ticket)
        {
            _messageResponse.data.numberPVPTicket += ticket;
        }

        public void AddPvpGame(int value = 1)
        {
            _messageResponse.data.numberPVPContesntGame += value;
        }

        public void AddPvpWinGame(int value = 1)
        {
            _messageResponse.data.numberPVPContestWinGame += value;
        }

        public void AddPvpContestSpendTicket(int value)
        {
            _messageResponse.data.numberPVPContestSpendTicket += value;
        }

        public void NewSeasonSetUp()
        {
            _messageResponse.data.numberPVPContestWinGame = default;
            _messageResponse.data.numberPVPContesntGame = default;
            _messageResponse.data.numberPVPContestSpendTicket = default;
        }
    }
}