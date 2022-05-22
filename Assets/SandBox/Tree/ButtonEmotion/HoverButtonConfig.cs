using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(fileName = "HoverButton", menuName = "ScriptableObject/HoverButton")]
public class HoverButtonConfig : ScriptableObject
{
    public HoverButtonSetting normal;
    public HoverButtonSetting moveY;
    public HoverButtonSetting chestIn;
    public HoverButtonSetting chestOut;
}


