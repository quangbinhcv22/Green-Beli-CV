using GRBEGame.UI.Screen.Inventory;
using GRBEGame.UI.Screen.Inventory.Fragment;
using TMPro;
using UnityEngine;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class TokenCoreViewText : MonoBehaviour, IMemberView<FragmentItemInfo>
    {
        [SerializeField] private FragmentItemCoreView ownItemCoreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string stringDefault = "-";
        [SerializeField] private string stringFormat = "{0:N0}";


        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
            ownItemCoreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault() => text.SetText(stringDefault);
        public void UpdateView(FragmentItemInfo data) => text.SetText(string.Format(stringFormat, data.count));
    }
}
