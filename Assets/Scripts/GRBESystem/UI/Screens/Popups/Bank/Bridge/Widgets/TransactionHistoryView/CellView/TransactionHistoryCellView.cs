using EnhancedUI.EnhancedScroller;
using Network.Service.Implement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace GRBESystem.UI.Screens.Popups.Bank.Bridge.Widgets.TransactionHistoryView.CellView
{
    public class TransactionHistoryCellView : EnhancedScrollerCellView
    {
        [System.Serializable]
        private struct FieldTexts
        {
            public TMP_Text type;
            public TMP_Text status;
            public TMP_Text value;
            public TMP_Text lastUpdated;
        }

        [SerializeField, Space] private FieldTexts fieldTexts;
        [SerializeField] private Image background;

        [SerializeField, Space] private TransactionHistorySuffixesSet suffixesSet;
        [SerializeField] private TransactionHistoryCellViewArtSet cellViewArtSet;


        public void UpdateView(TransactionHistoryResponse transactionHistory)
        {
            fieldTexts.type.SetText(transactionHistory.type.RemovingNonAlphaCharacters().ToTitleCase());
            fieldTexts.status.SetText(((BridgeTransactionStatus)transactionHistory.status).ToString());
            fieldTexts.value.SetText(FormattedValueChanged(transactionHistory.type.RemovingNonAlphaCharacters(), transactionHistory.quantity));
            fieldTexts.lastUpdated.SetText(ReFormattedLastUpdated(transactionHistory.createdTime));

            background.sprite = cellViewArtSet.GetSprite(transform.GetSiblingIndex());
        }

        private string FormattedValueChanged(string bridgeType, string value)
        {
            return $"{suffixesSet.GetSuffixes(bridgeType)} {int.Parse(value):N0}";
        }

        private string ReFormattedLastUpdated(string lastUpdated)
        {
            return lastUpdated.ToDateTime("dd-MMM-yyyy H:m:s").ToString("MMM dd hh:mm:ss tt");
        }
        
        public enum BridgeTransactionStatus
        {
            Unknown,
            Pending,
            Success,
            Cancel,
        }
    }
}