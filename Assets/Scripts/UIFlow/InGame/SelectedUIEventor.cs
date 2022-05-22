using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;

namespace UIFlow.InGame
{
    public class SelectedUIEventor : MonoBehaviour
    {
        [SerializeField] private UIId target;
        [SerializeField] private UnityEvent onSelected;
        [SerializeField] private UnityEvent onNormal;
        [SerializeField] private bool isSelecting;

        private void OnEnable()
        {
            EventManager.StartListening(EventName.UI.ActiveUIsChanged(), CheckUISelection);
            CheckUISelection();
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.ActiveUIsChanged(), CheckUISelection);
        }

        private void CheckUISelection()
        {
            var isSelectedTarget =  ActiveUIs.IsActive(target);
            if (isSelecting == isSelectedTarget) return;

            isSelecting = isSelectedTarget;
            (isSelecting ? onSelected : onNormal)?.Invoke();
        }
    }
}