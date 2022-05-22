using GEvent;
using TigerForge;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class ConfirmUnboxPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.StopListening(EventName.UI.Select<ConfirmUnboxPanel>(), () => gameObject.SetActive(true));
        }
        
        private void OnDisable()
        {
            EventManager.StartListening(EventName.UI.Select<ConfirmUnboxPanel>(), () => gameObject.SetActive(true));
        }
    }
}
