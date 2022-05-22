using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(100)]
public class ActiveTreeConfirmButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private bool isActive = true;

    private void Awake()=> button.onClick.AddListener(ButtonClicked);

        private void ButtonClicked()
    {
        isActive = !isActive;
        EventManager.EmitEventData(EventName.Select.ActiveTree,isActive);
    }
}
