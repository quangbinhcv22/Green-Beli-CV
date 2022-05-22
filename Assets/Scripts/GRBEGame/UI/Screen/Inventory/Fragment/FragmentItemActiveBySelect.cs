using GEvent;
using GRBEGame.UI.Screen.Inventory.Fragment;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemActiveBySelect : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;

        private FragmentItemInfo _fragmentItemInfo;
        

        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            EventManager.StartListening(EventName.UI.Select<FragmentItemInfo>(), OnItemSelected);
        }
        
        private void OnItemSelected()
        {
            var data = EventManager.GetData(EventName.UI.Select<FragmentItemInfo>());
            if (data is null || _fragmentItemInfo is null) SetActive(false);
            else
            {
                var fragmentItem = (FragmentItemInfo) data;
                SetActive(_fragmentItemInfo.type == fragmentItem.type);
            }
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }

        public void UpdateDefault()
        {
            _fragmentItemInfo = null;
            OnItemSelected();
        }

        public void UpdateView(FragmentItemInfo data)
        {
            _fragmentItemInfo = data;
            OnItemSelected();
        }
    }
}
