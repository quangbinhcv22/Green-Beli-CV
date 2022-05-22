using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class ActiveTreeImage : MonoBehaviour
{
    [SerializeField] private Image button;
    [SerializeField] private Sprite active;
    [SerializeField] private Sprite inactive;
    
    private void OnEnable()
    {
        EventManager.StartListening(EventName.Select.ActiveTree,UpdateView);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.ActiveTree,UpdateView);
    }

    private void UpdateView()
    {
        var isActiveData = EventManager.GetData(EventName.Select.ActiveTree);

        if (isActiveData is bool isActive)
        {
            button.sprite = isActive switch
            {
                true => active,
                false => inactive
            };
        }
    }
}
