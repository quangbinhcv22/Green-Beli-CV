using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CareTreeDeactiveButton : MonoBehaviour
{
    [SerializeField] private Slider water;
    [SerializeField] private Slider fertilizer;

    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image icon;
    [SerializeField] private FadeConfig fadeConfig;

    private void Awake()
    {
        UpdateView();
        water.onValueChanged.AddListener(_=> UpdateView());
        fertilizer.onValueChanged.AddListener(_=> UpdateView());
    }

    private void UpdateView()
    {
        if (water.value < 1 && fertilizer.value < 1)
        {
            button.interactable = false;
            text.DOFade(fadeConfig.blur.visible, fadeConfig.blur.duration).SetEase(fadeConfig.blur.ease);
            icon.DOFade(fadeConfig.blur.visible, fadeConfig.blur.duration).SetEase(fadeConfig.blur.ease);
        }
        else
        {
            button.interactable = true;
            text.DOFade(fadeConfig.normal.visible, fadeConfig.blur.duration).SetEase(fadeConfig.blur.ease);
            icon.DOFade(fadeConfig.normal.visible, fadeConfig.blur.duration).SetEase(fadeConfig.blur.ease);
        }
    }
}
