using Network.Service;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(200)]
public class TreeEventCheckButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake() => button.onClick.AddListener(CheckWhiteList);

    private void CheckWhiteList() => CheckWhiteListBuyTreeService.SendRequest(true,NetworkService.playerInfo.address);
}
