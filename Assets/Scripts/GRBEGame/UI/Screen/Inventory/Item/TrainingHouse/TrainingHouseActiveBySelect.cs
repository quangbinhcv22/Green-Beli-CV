using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class TrainingHouseActiveBySelect : MonoBehaviour, IMemberView<TrainingHouseItem>
    {
        [SerializeField] private TrainingHouseCoreView coreView;

        private ItemInventory _itemInventory;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            EventManager.StartListening(EventName.UI.Select<ItemInventory>(), OnItemSelected);
        }

        private void OnItemSelected()
        {
            var data = EventManager.GetData(EventName.UI.Select<ItemInventory>());
            if (data is null || _itemInventory is null) SetActive(false);
            else
            {
                var item = (ItemInventory) data;
                SetActive(_itemInventory.id == item.id);
            }
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }

        public void UpdateDefault()
        {
            _itemInventory = null;
            OnItemSelected();
        }

        public void UpdateView(TrainingHouseItem data)
        {
            _itemInventory = data;
            OnItemSelected();
        }
    }
}