using GRBEGame.Define;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemImage : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
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

        public void UpdateView(BoxItemInfo data)
        {
            image.sprite = artSet.GetFragmentIcon((FragmentType) data.type);
        }
        
        private void OnValidate()
        {
            Assert.IsNotNull(coreView);
            Assert.IsNotNull(image);
        }
    }
}
