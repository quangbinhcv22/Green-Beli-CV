using UIFlow;
using UnityEngine;
using UnityEngine.UI;

namespace SandBox.Battle
{
    public class PvPOpinionDisconnectButton : MonoBehaviour
    {
        [SerializeField] private UIRequest statusPopupRequest;
        
        
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnNext);
        }

        private void OnNext() => statusPopupRequest.SendRequest();
    }
}
