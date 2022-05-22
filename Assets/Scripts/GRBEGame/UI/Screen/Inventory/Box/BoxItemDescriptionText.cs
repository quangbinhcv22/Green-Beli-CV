using Network.Service;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemDescriptionText : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
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

        public void UpdateView(BoxItemInfo data)
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if (response.IsError) return;

            var description = response.data.inventory.GetFragmentInventory(data.type).description;
            text.SetText(string.Format(textFormat, description));
        }
    }
}
