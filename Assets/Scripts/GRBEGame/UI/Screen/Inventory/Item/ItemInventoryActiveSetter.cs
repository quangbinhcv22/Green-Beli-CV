using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryActiveSetter : MonoBehaviour, IMemberView<ItemInventory>
    {
        [SerializeField] private ItemInventoryCoreView ownItemCoreView;
        [SerializeField] private bool activeDefault;
        [SerializeField] private bool activeWhenUpdateView = true;


        private void Awake()
        {
            ownItemCoreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(activeDefault);
        }

        public void UpdateView(ItemInventory data)
        {
            SetActive(activeWhenUpdateView);
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}
