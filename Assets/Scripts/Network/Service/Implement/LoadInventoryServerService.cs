using System;
using System.Collections.Generic;
using GEvent;
using GNetwork;
using Network.Messages;
using Newtonsoft.Json;
using UnityEngine;


namespace Network.Service.Implement
{
    [CreateAssetMenu(fileName = nameof(LoadInventoryServerService), menuName = "NetworkAPI/LoadInventory")]
    public class LoadInventoryServerService : ScriptableObject, IServerAPI
    {
        private static LoadInventoryServerService Instance => NetworkApiManager.GetAPI<LoadInventoryServerService>();
        public static MessageResponse<InventoryResponse> Response => Instance._response;
        [NonSerialized] private MessageResponse<InventoryResponse> _response;


        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.LoadInventory).Send();
        }
        
        public string APIName => EventName.Server.LoadInventory;
        
        public void OnResponse(string message)
        {
            _response = JsonConvert.DeserializeObject<MessageResponse<InventoryResponse>>(message);
        }
    }
    
    [System.Serializable]
    public struct FragmentResponse
    {
        public int number;
        public int type;
    }
    
    [System.Serializable]
    public struct BoxResponse
    {
        public long id;
        public int type;
    }

    [System.Serializable]
    public struct MaterialResponse
    {
        public int type;
        public int number;
    }
    
    [System.Serializable]
    public struct PackResponse
    {
        public int number;
        public long id;
        public int type;
    }
    
    [System.Serializable]
    public struct ExpCardResponse
    {
        public string id;
        public int usedBattles;
        public int maxBattles;
        public int xBooster;
        public int star;
        public int rarity;
    }
    
    [System.Serializable]
    public struct FusionStoneResponse
    {
        public string id;
        public int usedPoints;
        public int maxPoints;
        public int element;
        public int star;
        public int rarity;
    }
    
    [System.Serializable]
    public struct TrainingHouseResponse
    {
        public string id;
        public int rooms;
        public int rarity;
        public List<TrainingHouseRoom> listOfRooms;
    }

    [System.Serializable]
    public struct TrainingHouseRoom
    {
        public int usedExp; 
        public int expCapacity; 
        public int star;
        public int rarity;
    }

    [System.Serializable]
    public class InventoryResponse
    {
        public List<FragmentResponse> fragments;

        public List<MaterialResponse> materials;

        public List<BoxResponse> boxes;
        public List<PackResponse> packs;
        
        public List<ExpCardResponse> expCards;
        public List<FusionStoneResponse> fusionStones;
        public List<TrainingHouseResponse> trainingHouses;
    }
}
