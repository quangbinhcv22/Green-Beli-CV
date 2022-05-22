using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.OpenPvpChest
{
    public class PvpRewardChest : MonoBehaviour
    {
        [SerializeField] private Image rewardChest;
        [SerializeField] private SpriteMask spriteMask;

        [Header("Sprite Set")] 
        [SerializeField] private Sprite goldChest;
        [SerializeField] private Sprite silverChest;

        private bool _isFirstUpdated;
        
        
        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame, UpdateView);
        }

        private void OnEnable()
        {
            if (EndGameServerService.Response.IsError is false && _isFirstUpdated is false)
                UpdateView();
        }

        private void UpdateView()
        {
            _isFirstUpdated = true;
            
            if(rewardChest)
                rewardChest.sprite = EndGameServerService.IsGoldChest() ? goldChest : silverChest;
            if (spriteMask)
                spriteMask.sprite = EndGameServerService.IsGoldChest() ? goldChest : silverChest;
        }
    }
}
