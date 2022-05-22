using GRBEGame.Define;
using GRBEGame.UI.Screen.Inventory.Fragment;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    [DefaultExecutionOrder(100)]
    public class FragmentItemImage : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
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

        public void UpdateView(FragmentItemInfo data)
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