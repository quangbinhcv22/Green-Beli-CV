using GRBEGame.Define;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory
{
    public class ItemInventoryActiveByTypeSetter : MonoBehaviour, IMemberView<ItemInventory>
    {
        [SerializeField] private ItemInventoryCoreView coreView;
        [SerializeField] private FragmentType itemType;
        [SerializeField] private bool isEnableDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(isEnableDefault);
        }

        public void UpdateView(ItemInventory data)
        {
            SetActive(data.itemInventoryType == itemType);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
