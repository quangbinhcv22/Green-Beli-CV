using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.Loading.Widgets.GameTip
{
    public class GameTipText : MonoBehaviour
    {
        [SerializeField] private TMP_Text tipText;
        [SerializeField] private GameTipsPreBuilt tipsPreBuilt;

        private void OnEnable()
        {
            tipText.text = tipsPreBuilt.GetTipRandom();
        }
    }
}