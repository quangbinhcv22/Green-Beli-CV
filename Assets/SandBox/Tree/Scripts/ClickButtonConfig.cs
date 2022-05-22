using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ClickButtonConfig", fileName = nameof(ClickButtonConfig))]
public class ClickButtonConfig : ScriptableObject
{
    public ClickButtonSetting normal;
    public ClickButtonSetting press;
    public ClickButtonSetting chestIn;
    public ClickButtonSetting chestOut;
    public ClickButtonSetting shake;
}