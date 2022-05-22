using QB.Collection;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Bank.PiggyBank.Widgets.RewardHistoryDateView
{
    [CreateAssetMenu(fileName = nameof(LockRewardSourceDescriptionLocalizeConfig),
        menuName = "ScriptableObject/Localize/LockRewardSourceDescription")]
    public class LockRewardSourceDescriptionLocalizeConfig : ScriptableObject
    {
        public DefaultableDictionary<string, string> config;
    }
}