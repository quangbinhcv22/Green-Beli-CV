using System;
using TMPro;
using UIFlow;
using UnityEngine;

namespace GRBEGame.UI.ConfirmPopup
{
    public class ConfirmPopup : MonoBehaviour
    {
        [SerializeField] private UIObject owner;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text contentText;
        [SerializeField] private ConfirmButton confirmButton;

        private void OnEnable()
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var uiData = owner.Data;

            if (uiData is ConfirmPopupData confirmData)
            {
                titleText.SetText(string.IsNullOrEmpty(confirmData.title) is false ? confirmData.title : "Confirm");
                contentText.SetText(confirmData.content);
                confirmButton.SetConfirmID(confirmData.id);
            }
        }
    }

    [Serializable]
    public class ConfirmPopupData
    {
        public ConfirmID id;
        public string title;
        public string content;
    }
}