using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialItemActiveBySelect : MonoBehaviour, IMemberView<MaterialInfo>
    {
        [SerializeField] private MaterialCoreView coreView;

        private MaterialInfo _materialItemInfo;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
            EventManager.StartListening(EventName.UI.Select<MaterialInfo>(), OnItemSelected);
        }

        private void OnItemSelected()
        {
            var data = EventManager.GetData(EventName.UI.Select<MaterialInfo>());
            if (data is null || _materialItemInfo is null) SetActive(false);
            else
            {
                var materialItem = (MaterialInfo) data;
                SetActive(_materialItemInfo.materialType == materialItem.materialType);
            }
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }

        public void UpdateDefault()
        {
            _materialItemInfo = null;
            OnItemSelected();
        }

        public void UpdateView(MaterialInfo data)
        {
            _materialItemInfo = data;
            OnItemSelected();
        }
    }
}
