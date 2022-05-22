using GRBEGame.UI.DataView;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Resource.PvpKey
{
    public class PvpKeyQuantityText : MonoBehaviour, IMemberView<PvpChest>
    {
        [SerializeField] private PvpKeyCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "{0}";
        [SerializeField] private string defaultString;

        private void Awake()
        {
            coreView.AddCallBackUpdateView(this);
        }

        public void UpdateDefault()
        {
            text.SetText(defaultString);
        }

        public void UpdateView(PvpChest data)
        {
            text.SetText(string.Format(textFormat, data.quantity));
        }
    }
}