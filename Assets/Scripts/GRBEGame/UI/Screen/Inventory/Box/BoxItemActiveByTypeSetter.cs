using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class BoxItemActiveByTypeSetter : MonoBehaviour, IMemberView<BoxItemInfo>
    {
        [SerializeField] private BoxItemCoreView coreView;
        [SerializeField] private BoxItemType boxItemType;
        [SerializeField] private bool isEnableDefault;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(isEnableDefault);
        }

        public void UpdateView(BoxItemInfo data)
        {
            SetActive(data.boxItemType == boxItemType);
        }

        private void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
