using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ConfirmUnpackPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.StopListening(EventName.UI.Select<ConfirmUnpackPanel>(), () => gameObject.SetActive(true));
        }

        private void OnDisable()
        {
            EventManager.StartListening(EventName.UI.Select<ConfirmUnpackPanel>(), () => gameObject.SetActive(true));
        }
    }
}
