using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    public class MaterialItemCountText : MonoBehaviour, IMemberView<MaterialInfo>
    {
        [SerializeField] private MaterialCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string defaultString = "-";


        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.text = defaultString;
        }

        public void UpdateView(MaterialInfo data)
        {
            text.SetText(string.Format(textFormat, data.count));
        }
    }
}
