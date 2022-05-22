using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryImage : MonoBehaviour, IMemberView<ItemInventory>
    {
        [SerializeField] private ItemInventoryCoreView coreView;
        [SerializeField] private Image image;
        [SerializeField] private FragmentArtSet artSet;
        [SerializeField] private Sprite defaultSprite;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            image.sprite = defaultSprite;
        }

        public void UpdateView(ItemInventory data)
        {
            image.sprite = artSet.GetFragmentIcon(data.itemInventoryType);
        }
    }
}
