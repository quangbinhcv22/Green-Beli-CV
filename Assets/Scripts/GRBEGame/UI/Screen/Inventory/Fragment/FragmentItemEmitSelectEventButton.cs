using GEvent;
using GRBEGame.UI.Screen.Inventory.Fragment;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemEmitSelectEventButton : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private Button button;

        private FragmentItemInfo _fragmentItemInfo;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            button.onClick.AddListener(EmitSelectEvent);
        }

        private void EmitSelectEvent()
        {
            if (_fragmentItemInfo is null) return;
            EventManager.EmitEventData(EventName.UI.Select<FragmentItemInfo>(), _fragmentItemInfo);
        }

        public void UpdateDefault()
        {
            _fragmentItemInfo = null;
        }

        public void UpdateView(FragmentItemInfo data)
        {
            _fragmentItemInfo = data;
        }
    }
}