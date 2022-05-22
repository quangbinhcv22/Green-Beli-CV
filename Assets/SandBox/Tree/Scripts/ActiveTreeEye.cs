using System.Collections;
using System.Collections.Generic;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class ActiveTreeEye : MonoBehaviour
{
    [SerializeField] private Image eye;
    [SerializeField] private Sprite activate;
    [SerializeField] private Sprite deactivate;
    
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
            eye.sprite = isActive switch
            {
                true => activate,
                false => deactivate
            };
        }
    }
}
