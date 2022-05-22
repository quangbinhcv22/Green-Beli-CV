using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialCoreViewActiveSetter : MonoBehaviour, IMemberView<MaterialInfo>
    {
        [SerializeField] private MaterialCoreView coreView;
        [SerializeField] private bool activeDefault;
        [SerializeField] private bool activeWhenUpdateView = true;


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            SetActive(activeDefault);
        }

        public void UpdateView(MaterialInfo data)
        {
            SetActive(activeWhenUpdateView);
        }

        private void SetActive(bool enable)
        {
            gameObject.SetActive(enable);
        }
    }
}
