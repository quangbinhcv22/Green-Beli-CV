using GRBEGame.UI.Screen.Inventory.Fragment;
using UnityEngine;
using UnityEngine.UI;


namespace GRBEGame.UI.Screen.Inventory
{
    public class FragmentPackageSliderReset : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView coreView;
        [SerializeField] private Slider slider;

        
        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            slider.value = default;
        }

        public void UpdateView(FragmentItemInfo data)
        {
            UpdateDefault();
        }
    }
}
