using System.Collections.Generic;
using GRBEGame.Define;
using UnityEngine;

namespace GRBEGame.Localization
{
    [CreateAssetMenu(fileName = nameof(FragmentRewardPriorityConfig), menuName = "ScriptableObject/Localization/FragmentRewardPriority")]
    public class FragmentRewardPriorityConfig : ScriptableObject
    {
        [SerializeField] private List<FragmentType> fragmentRewardPriorities;

        public int GetPriority(FragmentType type)
        {
            return fragmentRewardPriorities.FindIndex(rewardPriority => rewardPriority == type);
        }

        public int GetPriority(int type)
        {
            return GetPriority((FragmentType)type);
        }
    }
}

