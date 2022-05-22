using System.Collections;
using DG.Tweening;
using GScreen;
using UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.Loading.Widgets.LoadingBar
{
    [RequireComponent(typeof(ProcessBar))]
    public class LoadingBar : MonoBehaviour
    {
        [SerializeField] private ProcessBar processBar;

        private void Awake()
        {
            LoadingPanelReporter.Instance.OnLoading += OnLoad;
            
            processBar.durationTweenOnValueChange = LoadingPanelReporter.Instance.config.GetMainHallDelay();
        }

        private void OnEnable()
        {
            processBar.ResetView();
        }

        private void OnLoad(float loadingValue)
        {
            const int fullLoadingValue = 1;
            processBar.UpdateView(loadingValue, fullLoadingValue);
        }
        
    }
}