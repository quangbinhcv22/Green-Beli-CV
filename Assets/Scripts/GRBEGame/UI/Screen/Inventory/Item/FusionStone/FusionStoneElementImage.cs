using GRBESystem.Entity.Element;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FusionStoneElementImage : MonoBehaviour, IMemberView<FusionStoneItem>
    {
        [SerializeField] private FusionStoneCoreView coreView;
        [SerializeField] private Image icon;
        [SerializeField] private ElementArtSet artSet;
        [SerializeField] private Sprite defaultSprite;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetSprite(defaultSprite);
        }

        public void UpdateView(FusionStoneItem data)
        {
            SetSprite(artSet.GetSprite(data.element));
        }

        private void SetSprite(Sprite sprite)
        {
            icon.sprite = sprite;
        }
    }
}
