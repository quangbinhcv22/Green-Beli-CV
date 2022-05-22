using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SpecialTextConfig", fileName = nameof(SpecialTextConfig))]
public class SpecialTextConfig : ScriptableObject
{
    public SpecialTextSetting jump;
}