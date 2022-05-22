using Network.Service;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-500)]
public class GetTreeEventTime : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake() => button.onClick.AddListener(Setup);

    private static void Setup() => CheckWhiteListBuyTreeService.SendRequest(false, NetworkService.playerInfo.address);
}