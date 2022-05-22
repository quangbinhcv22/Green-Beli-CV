using TMPro;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    [RequireComponent(typeof(TMP_Text))]
    public class JackpotRewardText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string stringDefault;
        [SerializeField] private string stringFormat;
        

        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            
        }
    }
}
