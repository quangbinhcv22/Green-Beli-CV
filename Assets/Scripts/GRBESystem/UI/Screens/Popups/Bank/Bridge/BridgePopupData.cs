using System;
using System.Collections.Generic;
using GRBESystem.Entity;
using Manager.Inventory;
using Network.Web3;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bridge
{
    [Serializable]
    public struct BridgePopupData
    {
        public string address;
        
        public List<BridgeAction> bridgeActions;

        public static BridgePopupData Create(PlayerClientInfo playerInfo)
        {
            return new BridgePopupData()
            {
                address = playerInfo.address,
            };
        }
    }

    [Serializable]
    public struct BridgeAction
    {
        public BridgeType type;
        public BridgeStatus status;
        public int value;
        public BridgeTimeData time;
    }

    [Serializable]
    public struct BridgeTimeData
    {
        public long year;
        public int month;
        public int hour;
        public int minute;
        public int second;
    }

    [Serializable]
    public enum BridgeType
    {
        Deposit,
        Withdraw,
    }
    
    [Serializable]
    public enum BridgeStatus
    {
        Success,
        Fail,
    }
}
