using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Nation
{
    [RequireComponent(typeof(Button))]
    public class OpenNationConfirmButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            EventManager.StartListening(EventName.UI.Select<NationSelection>(), OnSelectNation);
            OnSelectNation();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<NationSelection>(), OnSelectNation);
        }

        private void OnSelectNation()
        {
            var nationSelection = EventManager.GetData(EventName.UI.Select<NationSelection>());
            var haveSelection = nationSelection != null;

            _button.interactable = haveSelection;
        }
    }
}