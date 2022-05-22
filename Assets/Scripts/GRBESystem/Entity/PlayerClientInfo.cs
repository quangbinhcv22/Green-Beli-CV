using System;
using Manager.Inventory;
using Network.Messages;

namespace GRBESystem.Entity
{
    [System.Serializable]
    public class PlayerClientInfo
    {
        public string address;
        public string authToken;
        public string id; // the same with address
        public EnergyInfo energyInfo;

        public Inventory inventory = new Inventory();

        public int Score { get; set; }

        //private string _heroAvatarId = "";

        public void SetDataFromLoginResponse(LoginResponse loginResponse)
        {
            id = loginResponse.id;
            
            inventory.SetMoney(MoneyType.GFruit, loginResponse.gfrToken);
            
            energyInfo = new EnergyInfo()
            {
                Current = loginResponse.energy,
                // Level = loginResponse.energyCapacityLevel,
            };
        }
    }
}