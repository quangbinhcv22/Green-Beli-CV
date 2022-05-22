using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.SkipAllBattle.Widgets.HistoryCellView
{
    public class SkipGameTokenReward : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image icon;


        private void SetActiveChild(bool isActive)
        {
            text.gameObject.SetActive(isActive);
            icon.gameObject.SetActive(isActive);
        }

        public void UpdateDefault() => SetActiveChild(default);
        
        public void UpdateView(int numberToken)
        {
            if (numberToken <= (int) default)
            {
                UpdateDefault();
                return;
            }
            SetActiveChild(true);
            text.SetText(numberToken.ToString("N0"));
        }
    }
}
