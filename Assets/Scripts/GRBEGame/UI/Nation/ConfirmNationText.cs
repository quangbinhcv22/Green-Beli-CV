using System;
using GEvent;
using Localization.Nation;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Nation
{
    public class ConfirmNationText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string format = "{0}";
        [SerializeField] private NationConfig nationConfig;

        private void OnEnable()
        {
            var nationSelection = EventManager.GetData(EventName.UI.Select<NationSelection>());
            var nationCode = (nationSelection is null ? new NationSelection() : (NationSelection)nationSelection)
                .nationCode;

            text.SetText(string.Format(format, nationConfig.GetNation(nationCode).fullName));
        }
    }
}