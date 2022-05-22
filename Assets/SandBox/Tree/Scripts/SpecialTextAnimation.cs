using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SpecialTextAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text[] texts;
    [SerializeField] private SpecialTextConfig specialTextSetting;

    private void Start()
    {
        TextJump(specialTextSetting);
    }

    private void TextJump(SpecialTextConfig setting)
    {
        StartCoroutine(SpaceTextJump(setting));
        
    }

    private IEnumerator SpaceTextJump(SpecialTextConfig setting)
    {
        var duration = setting.jump.spaceDuration;
        foreach (var text in texts)
        {
            text.transform.DOLocalMoveY(text.transform.localPosition.y + setting.jump.position, setting.jump.jumpDuration)
                .SetEase(setting.jump.ease).SetLoops(setting.jump.loops,setting.jump.loopType);
            yield return new WaitForSeconds(duration);
            duration -= 0.3f;
        }
    }
}


