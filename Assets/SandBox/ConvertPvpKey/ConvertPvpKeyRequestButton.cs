using Network.Service.Implement;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.ConvertPvpKey
{
    [RequireComponent(typeof(Button))]
    public class ConvertPvpKeyRequestButton : MonoBehaviour
    {
        private void Awake() => GetComponent<Button>().onClick.AddListener(SendRequest);
        private void SendRequest() => ConvertPvpKeyToRewardServerService.SendRequest();
    }
}
