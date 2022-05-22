using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObject/FadeConfig", fileName = nameof(FadeConfig))]
public class FadeConfig : ScriptableObject
{
    public FadeSetting normal;
    public FadeSetting invincible;
    public FadeSetting blur;
    public FadeSetting scale;
}
