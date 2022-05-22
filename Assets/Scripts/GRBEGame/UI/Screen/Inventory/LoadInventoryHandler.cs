using Network.Service;
using Network.Service.Implement;
using UnityEngine;


namespace GRBEGame.UI.Screen.Inventory
{
    public class LoadInventoryHandler : MonoBehaviour
    {
        private bool _isInventoryLoaded;

        private void Awake()
        {
            EmitLoadInventoryEvent();
        }

        private void OnEnable()
        {
            if (_isInventoryLoaded is false) EmitLoadInventoryEvent();
        }

        private void OnDisable()
        {
            _isInventoryLoaded = false;
        }

        private void EmitLoadInventoryEvent()
        {
            if (NetworkService.Instance.IsNotLogged()) return;

            _isInventoryLoaded = true;
            LoadInventoryServerService.SendRequest();
        }
    }
}
