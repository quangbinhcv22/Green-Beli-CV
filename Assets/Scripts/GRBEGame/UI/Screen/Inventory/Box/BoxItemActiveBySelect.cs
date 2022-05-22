using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemActiveBySelect : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;

        private BoxItemInfo _boxItemInfo;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            EventManager.StartListening(EventName.UI.Select<BoxItemInfo>(), OnItemSelected);
        }

        private void OnItemSelected()
        {
            var data = EventManager.GetData(EventName.UI.Select<BoxItemInfo>());
            if (data is null || _boxItemInfo is null) SetActive(false);
            else
            {
                var boxItem = (BoxItemInfo) data;
                SetActive(_boxItemInfo.id == boxItem.id);
            }
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }

        public void UpdateDefault()
        {
            _boxItemInfo = null;
            OnItemSelected();
        }

        public void UpdateView(BoxItemInfo data)
        {
            _boxItemInfo = data;
            OnItemSelected();
        }
    }
}
