using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(Button))]
    public class NumberMysteryChestRequestButton : MonoBehaviour
    {
        [SerializeField] private NumberChestOpenRequester numberChestRequest;


        private void Awake() => GetComponent<Button>().onClick.AddListener(OpenRequest);
        private void OpenRequest() =>
            EventManager.EmitEventData(EventName.UI.Select<NumberChestOpenRequester>(), numberChestRequest);
    }
}
