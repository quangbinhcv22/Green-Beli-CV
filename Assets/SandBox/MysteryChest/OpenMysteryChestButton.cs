using System.Collections.Generic;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(Button))]
    public class OpenMysteryChestButton : MonoBehaviour
    {
        private void Awake() => GetComponent<Button>().onClick.AddListener(OpenChestRequest);

        private void OpenChestRequest()
        {
            EventManager.EmitEvent(EventName.Server.SoundEffect);
            var data = EventManager.GetData(EventName.UI.Select<NumberChestOpenRequester>());
            if(data is null) return;

            var numberRequest = (NumberChestOpenRequester) data;
            OpenMysteryChestServerService.SendRequest(new OpenMysteryChestRequest
                {heroIds = new List<long> (), numberChest = numberRequest.numberChest});
        }
    }
}
