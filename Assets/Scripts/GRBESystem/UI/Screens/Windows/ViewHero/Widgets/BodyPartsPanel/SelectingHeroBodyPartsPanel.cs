using System;
using System.Collections.Generic;
using GRBESystem.Entity.Rarity;
using GRBESystem.UI.Screens.Windows.ViewHero.Widgets.BodyPartsPanel.Widgets.BodyPartSlot;
using Network.Messages.GetHeroList;
using Service.Client.SelectHero;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.ViewHero.Widgets.BodyPartsPanel
{
    public class SelectingHeroBodyPartsPanel : MonoBehaviour
    {
        [SerializeField, Space] private RarityArtConfig rarityArtConfig;

        [SerializeField, Space] private SelectHeroClientService selectHeroClientService;

        [SerializeField] private List<BodyPartSlotView> bodyPartViews;

        private void Awake()
        {
            selectHeroClientService.AddListenerResponse(UpdateView);
        }

        private void UpdateView()
        {
            var heroInfo = selectHeroClientService.GetEventEmitData();
            if(heroInfo.GetID() <= (long) default) return;
            
            var bodyParts = heroInfo.bodyParts;
            for (var i = 0; i < bodyParts.Count; i++)
            {
                var bodyPart = bodyParts[i];
                var rarityArt = rarityArtConfig.GetRarityArtPair(bodyPart.rarity == 0 ? 1 : bodyPart.rarity);
                    
                bodyPartViews[i].UpdateView(bodyPart);
                bodyPartViews[i].UpdateRarityBackground(rarityArt.levelBackground, rarityArt.mainBackground);
            }
           
        }
    }
}