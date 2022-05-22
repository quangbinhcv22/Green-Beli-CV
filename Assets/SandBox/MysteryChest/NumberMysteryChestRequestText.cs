using System;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(TMP_Text))]
    public class NumberMysteryChestRequestText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string stringDefault = "-";
        [SerializeField] private string stringFormat = "{0}";


        private void Awake() => text ??= GetComponent<TMP_Text>();

        private void OnEnable()
        {
            UpdateView();
            EventManager.StartListening(EventName.UI.Select<NumberChestOpenRequester>(), UpdateView);
        }

        private void UpdateView()
        {
            var data = EventManager.GetData(EventName.UI.Select<NumberChestOpenRequester>());
            if (data is null || ((NumberChestOpenRequester) data).numberChest == default)
            {
                text.SetText(stringDefault);
                return;
            }

            text.SetText(string.Format(stringFormat, ((NumberChestOpenRequester) data).numberChest));
        }
    }
}
