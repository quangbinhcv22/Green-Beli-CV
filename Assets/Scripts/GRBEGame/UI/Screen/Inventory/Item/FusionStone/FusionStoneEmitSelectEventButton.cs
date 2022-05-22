using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.Inventory
{
    public class FusionStoneEmitSelectEventButton : MonoBehaviour, IMemberView<FusionStoneItem>
    {
        [SerializeField] private FusionStoneCoreView coreView;
        [SerializeField] private Button button;

        private ItemInventory _itemInventory;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            button.onClick.AddListener(EmitSelectEvent);
        }

        private void EmitSelectEvent()
        {
            if (_itemInventory is null) return;
            EventManager.EmitEventData(EventName.UI.Select<ItemInventory>(), _itemInventory);
        }

        public void UpdateDefault()
        {
            _itemInventory = null;
        }

        public void UpdateView(FusionStoneItem data)
        {
            _itemInventory = data;
        }
    }
}
