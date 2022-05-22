using System;
using DG.Tweening;
using TMPro;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObject/TextConfig", fileName = nameof(TextConfig))]
public class TextConfig : ScriptableObject
{
    public TextSetting normal;
    public TextSetting hover;
}

[Serializable]
public class TextSetting
{
    public float size;
    public Color color;
    public ColorMode colorMode;
    public float duration;
    public Ease ease;
}
