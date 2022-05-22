using TMPro;
using UnityEngine;

namespace GRBEGame.UI.Widget.Application
{
    [RequireComponent(typeof(TMP_Text))]
    public class ApplicationVersionText : MonoBehaviour
    {
        [SerializeField] private string versionFormat = "{0}";

        private void Awake()
        {
            GetComponent<TMP_Text>().SetText(string.Format(versionFormat, UnityEngine.Application.version));
        }
    }
}