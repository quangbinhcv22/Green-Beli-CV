using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemTitleImage : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
        [SerializeField] private Image icon;
        [SerializeField] private BoxItemTypeArtSet boxItemTypeArtSet;
        [SerializeField] private Sprite spriteDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetSprite(spriteDefault);
        }

        public void UpdateView(BoxItemInfo data)
        {
            SetSprite(boxItemTypeArtSet.GetIcon(data.boxItemType));
        }

        private void SetSprite(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}
