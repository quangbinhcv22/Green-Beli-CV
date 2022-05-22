using Network.Service;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemTokenText : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private BoxItemType boxItemType;
        [SerializeField] private BoxItemMoneyType moneyType;
        [SerializeField] private string formatString = "{0}";
        [SerializeField] private string defaultString;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(defaultString);
        }

        public void UpdateView(BoxItemInfo data)
        {
            var loadGameResponse = NetworkService.Instance.services.loadGameConfig.Response;
            if (NetworkService.Instance.IsNotLogged() || loadGameResponse.IsError)
            {
                UpdateDefault();
                return;
            }

            text.SetText(string.Format(formatString, GetMoney(data)));
        }

        private long GetMoney(BoxItemInfo data)
        {
            if (boxItemType != data.boxItemType) return default;

            var inventory = NetworkService.Instance.services.loadGameConfig.Response.data.inventory;
            switch (boxItemType)
            {
                case BoxItemType.Box:
                {
                    return moneyType == BoxItemMoneyType.Gfruit
                        ? inventory.GetPriceToUnbox(data.type).gFruit
                        : inventory.GetPriceToUnbox(data.type).grbe;
                }
                case BoxItemType.Pack:
                {
                    return moneyType == BoxItemMoneyType.Gfruit
                        ? inventory.GetPriceToUnpack(data.type).gFruit
                        : inventory.GetPriceToUnpack(data.type).grbe;
                }
                default:
                    return default;
            }
        }
    }

    [System.Serializable]
    public enum BoxItemMoneyType
    {
        Gfruit = 0,
        Grbe = 1,
    }
}
