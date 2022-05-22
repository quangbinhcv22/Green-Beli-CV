using System.Collections.Generic;
using GRBEGame.Define;
using GRBEGame.UI.Screen.Inventory.Fragment;
using Network.Service;
using TMPro;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentItemAssembleGrbeText : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string formatString = "{0}";
        [SerializeField] private string defaultString = "-";
        
        [SerializeField, Space] private List<FragmentType> ignoreTypeList;


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
            if (NetworkService.Instance.IsNotLogged() || response.IsError || IsIgnoreType((FragmentType) data.type))
            {
                UpdateDefault();
                return;
            }

            text.SetText(string.Format(formatString,
                response.data.inventory.GetPriceToAssemble(data.type).grbe.ToString("N0")));
        }
        
        private bool IsIgnoreType(FragmentType fragmentType)
        {
            var isIgnoreType = false;
            ignoreTypeList.ForEach(item =>
            {
                if (item == fragmentType) isIgnoreType = true;
            });

            return isIgnoreType;
        }
    }
}
