using System;
using System.Collections.Generic;
using System.Linq;
using GEvent;
using Network.Messages;
using Network.Messages.LoadGame;
using UnityEngine;

namespace GNetwork
{
    [CreateAssetMenu(menuName = "NetworkAPI/GetListTree", fileName = nameof(GetListTreeServerService))]
    public class GetListTreeServerService : ScriptableObject, IServerAPI
    {
        private static GetListTreeServerService Instance => NetworkApiManager.GetAPI<GetListTreeServerService>();

        [SerializeField] private MessageResponse<List<Tree>> _response;
        public static MessageResponse<List<Tree>> Response => Instance._response;

        public static List<Tree> StageOneResponse => SoftStageOne();
        
        private static List<Tree> _oldTress;

        
        public static void SendRequest()
        {
            Message.Instance().SetId(EventName.Server.GetListTree).Send();
        }

        public string APIName => EventName.Server.GetListTree;

        private static List<Tree> SoftStageOne()
        {
            return Instance._response.data.Where(tree => tree.state == 1).ToList();
        }
        
        public void OnResponse(string message)
        {
            if (_response.data != null) _oldTress = _response.data;

            _response = JsonUtility.FromJson<MessageResponse<List<Tree>>>(message);

            if (_response.IsError) return;

            _response.data.RemoveAll(tree => string.IsNullOrEmpty(tree.id.ToString()) || tree.Element == 0);
            _oldTress ??= _response.data;
        }
        
        public static List<Tree> NewTrees()
        {
            if (Response.data is null || _oldTress is null) return new List<Tree>();

            var oldTreeIds = _oldTress.Select(tree => tree.id);
            return Response.data.Where(tree => oldTreeIds.Contains(tree.id) is false).ToList();
        }

        public static long GetPlantHeroIds(int treeId)
        {
            return Response.data.Find(tree => tree.id == treeId).plantHeroIds;

        }
        
        public static int GetTreeHealth(int treeId)
        {
            return Response.data.Find(tree => tree.id == treeId).healthPoint;
        }
        
        public static void SetTreeHealth(int treeId)
        {
            var tree = Response.data.Find(tree => tree.id == treeId);
            tree.healthPoint--;
        }

        public static int GetTotalFruits(int treeId)
        {
            var silverFruits = Response.data.Find(tree => tree.id == treeId).fruits[(int) FruitType.Silver].quantity;
            var goldFruits = Response.data.Find(tree => tree.id == treeId).fruits[(int) FruitType.Gold].quantity;
            return silverFruits + goldFruits;
            
            return Response.data.Find(tree => tree.id == treeId).totalFruits;
    }

        public static void SetTotalFruits(int treeId,int value)
        {
            
            var tree = Response.data.Find(tree => tree.id == treeId);
            tree.totalFruits += value;
        }
        
        public static void ResetTotalFruits(int treeId)
        {
            var tree = Response.data.Find(tree => tree.id == treeId);
            tree.totalFruits = 0;
        }
    }

    public enum TreeStatus
    {
        Active = 1,
        Inactive = 2,
    }

    [Serializable]
    public class Fruits
    {
        public FruitType fruitRare;
        public int quantity;
    }

    public enum FruitType
    {
        Gold = 1,
        Silver = 2
    }
}