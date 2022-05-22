using UnityEngine;
using UnityEngine.UI;

namespace UIFlow.InGame
{    [RequireComponent(typeof(Button))]
    public class RequestUIButton : MonoBehaviour
    {
        [SerializeField] private UIRequest request;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(SendRequest);
        }

        public void SendRequest() => request.SendRequest();
    }
}