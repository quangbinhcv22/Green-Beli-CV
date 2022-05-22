using Network.Service;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialItemDescriptionText : MonoBehaviour, IMemberView<MaterialInfo>
    {
        [SerializeField] private MaterialCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string stringDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(stringDefault);
        }

        public void UpdateView(MaterialInfo data)
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if (response.IsError) return;

            var description = response.data.inventory.GetMaterialInventoryInfo(data.type).description;
            text.SetText(string.Format(textFormat, description));
        }
    }
}
