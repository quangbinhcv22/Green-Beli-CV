using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class TrainingHouseRarityBackground : MonoBehaviour, IMemberView<TrainingHouseItem>
    {
        [SerializeField] private TrainingHouseCoreView coreView;
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

        public void UpdateView(TrainingHouseItem data)
        {
            SetSprite(artSet.GetBackground(data.rarity));
        }

        private void SetSprite(Sprite sprite)
        {
            background.sprite = sprite;
        }
    }
}
