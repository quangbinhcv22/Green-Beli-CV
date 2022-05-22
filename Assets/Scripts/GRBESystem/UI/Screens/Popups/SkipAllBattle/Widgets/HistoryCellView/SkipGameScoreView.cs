using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.HistoryCellView
{
    public class SkipGameScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text damageText;
        [SerializeField] private TMP_Text lastHitText;
        [SerializeField] private TMP_Text totalText;


        public void UpdateView(int damage = default, int lastHit = default, int total = default)
        {
            damageText.SetText(damage.ToString());
            lastHitText.SetText(lastHit.ToString());
            totalText.SetText(total.ToString());
        }
    }
}
