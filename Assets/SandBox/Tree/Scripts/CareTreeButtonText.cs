using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GEvent;
using TigerForge;
using TMPro;
using UnityEngine;

public class CareTreeButtonText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private FadeConfig fadeConfig;

    private void OnEnable() =>
        EventManager.StartListening(EventName.Select.ActiveTree,UpdateView);
    
    
    private void OnDisable()=>
        EventManager.StopListening(EventName.Select.ActiveTree,UpdateView);
    

    private void UpdateView()
    {
        var isActiveData = EventManager.GetData(EventName.Select.ActiveTree);

        if (isActiveData is bool isActive)
        {
            switch (isActive)
            {
                case true: NormalIcon(); break;
                case false: BlurIcon(); break;
            }
        }
    }

    private void NormalIcon()=>
        text.DOFade(fadeConfig.normal.visible, fadeConfig.blur.duration);
    

    private void BlurIcon()=>
        text.DOFade(fadeConfig.blur.visible, fadeConfig.blur.duration);
    
}
