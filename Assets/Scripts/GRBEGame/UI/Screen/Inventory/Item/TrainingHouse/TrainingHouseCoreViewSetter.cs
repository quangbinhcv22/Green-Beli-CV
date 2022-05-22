using GRBEGame.Define;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class TrainingHouseCoreViewSetter : MonoBehaviour, IMemberView<ItemInventory>
    {
        [SerializeField] private ItemInventoryCoreView itemCoreView;
        [SerializeField] private TrainingHouseCoreView trainingHouseCoreView;


        private void Awake()
        {
            itemCoreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            trainingHouseCoreView.UpdateDefault();
        }

        public void UpdateView(ItemInventory data)
        {
            if (data.itemInventoryType != FragmentType.TrainingHouse)
            {
                UpdateDefault();
                return;
            }

            trainingHouseCoreView.UpdateView((TrainingHouseItem) data);
        }
    }
}
