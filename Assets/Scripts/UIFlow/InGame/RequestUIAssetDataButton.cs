using UnityEngine;
using UnityEngine.UI;

namespace UIFlow.InGame
{
    [RequireComponent(typeof(Button))]
    public class RequestUIAssetDataButton : MonoBehaviour
    {
        [SerializeField] private UIRequest request;
        [SerializeField] private Object data;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SendRequest);
        }

        private void SendRequest()
        {
            request.data = data;
            request.SendRequest();
        }
    }
}