using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Fragment
{
    public class FragmentItemCoreViewActiveSetter : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView ownItemCoreView;
        [SerializeField] private bool activeDefault;
        [SerializeField] private bool activeWhenUpdateView = true;


        private void Awake()
        {
            ownItemCoreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(activeDefault);
        }

        public void UpdateView(FragmentItemInfo data)
        {
            SetActive(activeWhenUpdateView);
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}