using GEvent;
using Network.Service;
using TigerForge;
using UnityEngine;

namespace Manager.Resource.Assets
{
    public class ThemeChanger : MonoBehaviour
    {
        [SerializeField] private Theme theme;

        private void OnEnable()
        {
            EventManager.EmitEventData(EventName.WidgetEvent.CHANGE_THEME, data: theme);
        }
    }
}