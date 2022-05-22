using GRBEGame.Define;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory
{
    public class FusionStoneCoreViewSetter : MonoBehaviour, IMemberView<ItemInventory>
    {
        [SerializeField] private ItemInventoryCoreView itemCoreView;
        [SerializeField] private FusionStoneCoreView fusionStoneCoreView;


        private void Awake()
        {
            itemCoreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            fusionStoneCoreView.UpdateDefault();
        }

        public void UpdateView(ItemInventory data)
        {
            if (data.itemInventoryType != FragmentType.FusionStone)
            {
                UpdateDefault();
                return;
            }

            fusionStoneCoreView.UpdateView((FusionStoneItem) data);
        }
    }
}
