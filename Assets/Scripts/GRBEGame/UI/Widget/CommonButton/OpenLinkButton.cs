using UnityEngine;
using UnityEngine.UI;

namespace GRBEGame.UI.Widget.CommonButton
{
    [RequireComponent(typeof(Button))]
    public class OpenLinkButton : MonoBehaviour
    {
        [SerializeField] private string url;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => UnityEngine.Application.OpenURL(url));
        }

        public void SetUrl(string newUrl)
        {
            url = newUrl;
        }
    }
}