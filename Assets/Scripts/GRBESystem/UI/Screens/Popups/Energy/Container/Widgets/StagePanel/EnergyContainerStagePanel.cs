using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Energy.Container.Widgets.StagePanel
{
    public class EnergyContainerStagePanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentLevelText;
        [SerializeField] private TMP_Text nextLevelText;

        [SerializeField, Space] private TMP_Text maxLevelText;

        [SerializeField, Space] private GameObject currentStage;
        [SerializeField] private GameObject maxStage;


        public void UpdateViewStage(string currentValue, string nextValue)
        {
            currentStage.SetActive(true);
            maxStage.SetActive(false);
                
            currentLevelText.text = currentValue;
            nextLevelText.text = nextValue;
        }
        
        public void UpdateViewMaxStage(string maxValue)
        {
            currentStage.SetActive(false);
            maxStage.SetActive(true);
                
            maxLevelText.text = maxValue;
        }
    }
}
