using System.Collections;
using UnityEngine;

namespace Network.Controller
{
    public class NetworkConnector : MonoBehaviour
    {
        [SerializeField] private float delay = 1f;

        private void Start()
        {
            StartCoroutine(ConnectedToServer());
        }

        IEnumerator ConnectedToServer()
        {
            yield return new WaitForSeconds(delay);
            WebSocketController.Instance.Connect();
        }
    }
}