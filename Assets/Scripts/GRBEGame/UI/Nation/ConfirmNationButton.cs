using GEvent;
using Network.Service;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Nation
{
    [RequireComponent(typeof(Button))]
    public class ConfirmNationButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(ChangeNation);
        }

        private void ChangeNation()
        {
            var nationSelection = EventManager.GetData(EventName.UI.Select<NationSelection>());
            if (nationSelection is null) return;

            var nationCode = ((NationSelection)nationSelection).nationCode;
            NetworkService.Instance.services.setNation.SendRequest(new NationRequest { nationCode = nationCode });
        }
    }
}