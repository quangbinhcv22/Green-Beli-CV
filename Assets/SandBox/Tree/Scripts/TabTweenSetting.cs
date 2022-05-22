using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TabTweenConfig", fileName = nameof(TabTweenSetting))]
public class TabTweenSetting : ScriptableObject
{
    public float duration;
    public Color textColor;
    public float textSize;
    public Ease ease;
    public Vector3 position;
}
