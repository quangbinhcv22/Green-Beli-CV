using System;
using GEvent;
using Localization.Nation;
using Network.Service;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Nation
{
    public class NationCellView : MonoBehaviour
    {
        [SerializeField] private string nationCode;

        [SerializeField] [Space] private Image flag;
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private NationConfig nationConfig;

        [SerializeField] [Space] private Button button;
        [SerializeField] private GameObject currentObject;
        [SerializeField] private GameObject selectingObject;

        private void Awake()
        {
            button.onClick.AddListener(SelectNation);
            
            EventManager.StartListening(EventName.UI.Select<NationSelection>(), OnSelectNation);
            EventManager.StartListening(EventName.Server.SetNation, UpdateCurrentStatus);
        }

        private void OnEnable()
        {
            UpdateView(nationCode);
        }

        private void SelectNation()
        {
            var nationSelection = new NationSelection { nationCode = nationCode };
            EventManager.EmitEventData(EventName.UI.Select<NationSelection>(), nationSelection);
        }

        private void OnSelectNation()
        {
            var nationSelection = EventManager.GetData(EventName.UI.Select<NationSelection>());
            var isSelected = nationSelection != null && ((NationSelection)nationSelection)?.nationCode == nationCode;
            
            selectingObject.SetActive(isSelected);
        }

        public void UpdateView(string nationCode)
        {
            this.nationCode = nationCode;
            var nation = nationConfig.GetNation(nationCode);

            flag.sprite = nation.art;
            nameText.SetText(nation.fullName);

            UpdateCurrentStatus();
            OnSelectNation();
        }

        private void UpdateCurrentStatus()
        {
            var loginResponse = NetworkService.Instance.services.login.MessageResponse;
            if (loginResponse.IsError) return;

            var currentNation = NetworkService.Instance.services.login.MessageResponse.data.nation;
            var isSelfCurrent = nationCode == currentNation;

            currentObject.SetActive(isSelfCurrent);
            button.interactable = !isSelfCurrent;
        }
    }

    [Serializable]
    public class NationSelection
    {
        public string nationCode;
    }
}