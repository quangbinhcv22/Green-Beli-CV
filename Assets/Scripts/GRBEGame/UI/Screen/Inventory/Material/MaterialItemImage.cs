using GRBEGame.Define;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialItemImage : MonoBehaviour, IMemberView<MaterialInfo>
    {
        [SerializeField] private MaterialCoreView coreView;
        [SerializeField] private Image image;
        [SerializeField] private MaterialArtSet artSet;
        [SerializeField] private Sprite defaultSprite;

        
        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }
        
        public void UpdateDefault()
        {
            image.sprite = defaultSprite;
        }

        public void UpdateView(MaterialInfo data)
        {
            image.sprite = artSet.GetFragmentIcon((MaterialType) data.type);
        }
    }
}
