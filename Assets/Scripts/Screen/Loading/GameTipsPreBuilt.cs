using System.Collections.Generic;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Loading.Widgets.GameTip
{
    [CreateAssetMenu(fileName = "GameTipsPreBuilt", menuName = "ScriptableObjects/Screen/Loading/GameTipsPreBuilt")]
    public class GameTipsPreBuilt : ScriptableObject
    {
        [SerializeField] private List<string> tips;

        public string GetTipRandom()
        {
            return tips[Random.Range(0, tips.Count)];
        }
    }
}