using UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Setting.General.Widgets
{
    public class SliderInteractableBySwitcherSetter : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private SwitchHandler switchHandler;


        private void Awake() => switchHandler.onValueChanged.AddListener(OnValueChange);
        private void OnValueChange(bool isOn) => slider.interactable = isOn;
    }
}
