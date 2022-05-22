using GRBEGame.UI.DataView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.LuckyMen
{
    [RequireComponent(typeof(Slider))]
    public class SliderCoreView : MonoBehaviour, ICoreView<float>
    {
        [SerializeField] private float minValue;

        private Slider _slider;
        private UnityAction _onUpdateDefault;
        private UnityAction<float> _onUpdateView;


        public float MaxValue => _slider.maxValue + minValue;
        
        private void Awake()
        {
            _slider ??= GetComponent<Slider>();
            _slider.onValueChanged.AddListener(UpdateView);
        }

        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(float data)
        {
            _onUpdateView?.Invoke(data + minValue);
        }

        public void AddCallBackUpdateView(IMemberView<float> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
