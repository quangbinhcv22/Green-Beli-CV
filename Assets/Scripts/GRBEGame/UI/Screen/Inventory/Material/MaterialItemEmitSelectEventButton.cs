using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialItemEmitSelectEventButton : MonoBehaviour, IMemberView<MaterialInfo>
    {
        [SerializeField] private MaterialCoreView coreView;
        [SerializeField] private Button button;

        private MaterialInfo _materialItemInfo;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            button.onClick.AddListener(EmitSelectEvent);
        }

        private void EmitSelectEvent()
        {
            if (_materialItemInfo is null) return;
            EventManager.EmitEventData(EventName.UI.Select<MaterialInfo>(), _materialItemInfo);
        }

        public void UpdateDefault()
        {
            _materialItemInfo = null;
        }

        public void UpdateView(MaterialInfo data)
        {
            _materialItemInfo = data;
        }
    }
}
