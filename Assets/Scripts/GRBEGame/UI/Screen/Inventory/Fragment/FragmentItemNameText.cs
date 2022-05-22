using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemNameText : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
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

        public void UpdateView(FragmentItemInfo data)
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if (response.IsError) return;

            var nameItem = response.data.inventory.GetFragmentInventory(data.type).fragmentName;
            text.SetText(string.Format(textFormat, nameItem));
        }
    }
}
