using GRBEGame.Define;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ExpCardCoreViewSetter : MonoBehaviour, IMemberView<ItemInventory>
    {
        [SerializeField] private ItemInventoryCoreView itemCoreView;
        [SerializeField] private ExpCardCoreView expCardCoreView;


        private void Awake()
        {
            itemCoreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            expCardCoreView.UpdateDefault();
        }

        public void UpdateView(ItemInventory data)
        {
            if (data.itemInventoryType != FragmentType.ExpCard)
            {
                UpdateDefault();
                return;
            }

            expCardCoreView.UpdateView((ExpCardItem) data);
        }
    }
}
