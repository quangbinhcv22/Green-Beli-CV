using UnityEngine;

namespace Config.Mechanism
{
    [CreateAssetMenu(fileName = nameof(ElementBuffConfig), menuName = "ScriptableObject/MechanismConfig/ElementBuffConfig")]
    public class ElementBuffConfig : ScriptableObject
    {
        public float sameBossElement;
        public float mutualSubHeroFactor;
        public float nonMutualSubHeroFactor;
    }
}