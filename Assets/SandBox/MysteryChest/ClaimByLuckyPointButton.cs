using Network.Service.Implement;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.MysteryChest
{
    [RequireComponent(typeof(Button))]
    public class ClaimByLuckyPointButton : MonoBehaviour
    {
        private void Awake() => GetComponent<Button>().onClick.AddListener(OnClaimByLuckyPoint);

        private void OnClaimByLuckyPoint() => ClaimMysteryChestLuckyPointServerService.SendRequest();
    }
}
