using System.Collections.Generic;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;

namespace SandBox.MysteryChest
{
    public class OpenMysteryChestReceivedPanel : MonoBehaviour
    {
        [SerializeField] private Transform contentParent;
        [SerializeField] private MysteryItemViewer itemPrefab;
        
        private List<MysteryItemViewer> _items;

        
        private void Awake()
        {
            _items = new List<MysteryItemViewer>();
        }

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.Server.OpenMysteryChest, UpdateView);
        }

        private void UpdateView()
        {
            contentParent.gameObject.SetActive(false);
            
            if(OpenMysteryChestServerService.Response.IsError)
            {
                contentParent.gameObject.SetActive(false);
                return;
            }

            if (_items.Count < OpenMysteryChestServerService.Data.Count)
                for (int i = _items.Count; i < OpenMysteryChestServerService.Data.Count; i++)
                {
                    _items.Add(Instantiate(itemPrefab, contentParent));
                }

            OnClaimReward();
        }

        private void OnClaimReward()
        {
            contentParent.gameObject.SetActive(true);
            
            for (int i = 0; i < _items.Count; i++)
            {
                if (i >= OpenMysteryChestServerService.Data.Count)
                    _items[i].gameObject.SetActive(false);
                else
                {
                    _items[i].gameObject.SetActive(true);
                    _items[i].UpdateView(OpenMysteryChestServerService.Data[i]);
                }
            }
        }
    }
}
