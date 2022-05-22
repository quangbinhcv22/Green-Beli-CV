using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemPackGFruitText : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string formatString = "{0}";
        [SerializeField] private string defaultString = "-";

        
        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(string.Format(formatString, defaultString));
        }

        public void UpdateView(FragmentItemInfo data)
        {
            var response = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || response.IsError)
            {
                UpdateDefault();
                return;
            }

            var gFruit = response.data.inventory.GetPriceToPack(data.type).gFruit;
            text.SetText(string.Format(formatString, gFruit.ToString("N0")));
        }
    }
}
