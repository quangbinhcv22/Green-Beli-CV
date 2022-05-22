using Network.Messages.GetPvpContestDetail;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.DataView.Pvp
{
    public class PvpPlayerNumberFreeRewardText : MonoBehaviour, IMemberView<PvpPlayerInfo>
    {
        [SerializeField] private PvpPlayerCoreView coreView;
        [SerializeField] private TMP_Text text;
        [SerializeField] private float defaultValue;
        [SerializeField] private string valueFormat = "{0:P2}";

        private void Awake() => coreView.AddCallBackUpdateView(this);

        public void UpdateDefault() => UpdateValueText(defaultValue);

        public void UpdateView(PvpPlayerInfo data) => UpdateValueText(data.numberReceivedFreeMaterial);

        private void UpdateValueText(float value) => text.SetText(string.Format(valueFormat, value));
    }
}