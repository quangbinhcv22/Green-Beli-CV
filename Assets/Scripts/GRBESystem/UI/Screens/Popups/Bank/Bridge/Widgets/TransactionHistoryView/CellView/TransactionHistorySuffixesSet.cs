using System.Collections.Generic;
using GRBESystem.UI.Screens.Popups.Bridge;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.Bridge.Widgets.TransactionHistoryView.CellView
{
    [CreateAssetMenu(fileName = "TransactionHistorySuffixesSet", menuName = "ScriptableObjects/ValuePair/TransactionHistorySuffixes")]
    public class TransactionHistorySuffixesSet : ScriptableObject
    {
        [System.Serializable]
        private struct SuffixesPair
        {
            public BridgeType type;
            public string suffixes;
        }

        [SerializeField] private List<SuffixesPair> statusSuffixesPairs;

        public string GetSuffixes( BridgeType type)
        {
            return statusSuffixesPairs.Find(pair => pair.type == type).suffixes;
        }
        
        public string GetSuffixes( string type)
        {
            return statusSuffixesPairs.Find(pair => pair.type.ToString().ToLower().Equals(type.ToLower())).suffixes;
        }
    }
}