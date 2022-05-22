using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemEmitSelectEventButton : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
        [SerializeField] private Button button;

        private BoxItemInfo _boxItemInfo;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            button.onClick.AddListener(EmitSelectEvent);
        }

        private void EmitSelectEvent()
        {
            if (_boxItemInfo is null) return;
            EventManager.EmitEventData(EventName.UI.Select<BoxItemInfo>(), _boxItemInfo);
        }

        public void UpdateDefault()
        {
            _boxItemInfo = null;
        }

        public void UpdateView(BoxItemInfo data)
        {
            _boxItemInfo = data;
        }
    }
}
