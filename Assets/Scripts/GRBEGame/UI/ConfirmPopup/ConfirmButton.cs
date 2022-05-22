using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.ConfirmPopup
{
    [RequireComponent(typeof(Button))]
    public class ConfirmButton : MonoBehaviour
    {
        [SerializeField] private ConfirmID confirmID;
        public void SetConfirmID(ConfirmID value) => confirmID = value;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Confirm);
        }

        private void Confirm()
        {
            EventManager.EmitEventData(EventName.Mechanism.Confirm, confirmID);
        }
    }
}