using System.Collections;
using System.Collections.Generic;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.Inventory
{
    public class ExpCardRarityBackground : MonoBehaviour, IMemberView<ExpCardItem>
    {
        [SerializeField] private ExpCardCoreView coreView;
        [SerializeField] private Image background;
        [SerializeField] private ItemInventoryBackgroundArtSet artSet;
        [SerializeField] private Sprite defaultSprite;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetSprite(defaultSprite);
        }

        public void UpdateView(ExpCardItem data)
        {
            SetSprite(artSet.GetBackground(data.rarity));
        }

        private void SetSprite(Sprite sprite)
        {
            background.sprite = sprite;
        }
    }
}
