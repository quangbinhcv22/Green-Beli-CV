using Network.Web3;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.Withdraw
{
    [CreateAssetMenu(fileName = "WithdrawConfig", menuName = "ScriptableObjects/WithdrawConfig")]
    public class WithdrawConfig : ScriptableObject
    {
        public int minValue = 100000;
        public int maxValue = 500000;

        public WithDrawTokenInfo info;
    }
}