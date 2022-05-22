using UnityEngine;
using UnityEngine.EventSystems;

public class DisableEventSystem : MonoBehaviour
{
    private EventSystem _eventSystem;

    private void Awake() => _eventSystem = EventSystem.current;

    private void OnEnable() => EnableEventSystem(false);

    private void OnDisable() => EnableEventSystem(true);

    private void EnableEventSystem(bool isEnable)
    {
        if (_eventSystem) _eventSystem.enabled = isEnable;
    }
}