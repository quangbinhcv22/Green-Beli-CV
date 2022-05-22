using Network.Service;
using Network.Service.Implement;
using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Widget.ClientInfo
{
    public class LatestVersionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] [TextArea] private string textFormat = "{0}";

        private void OnEnable()
        {
            if (GetLatestClientReleaseServerService.Response.IsError) return;

            var latestVersion = GetLatestClientReleaseServerService.GetLatestPlatformOnThisDeviceRelease();
            text.SetText(string.Format(textFormat, latestVersion));
        }
    }
}