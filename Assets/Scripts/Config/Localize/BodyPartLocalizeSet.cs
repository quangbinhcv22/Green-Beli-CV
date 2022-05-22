using GRBESystem.Definitions.BodyPart.Index;
using QB.Collection;
using UnityEngine;

namespace Config.Localize
{
    [CreateAssetMenu(fileName = nameof(BodyPartLocalizeSet), menuName = "ScriptableObject/Localize/BodyPart")]
    public class BodyPartLocalizeSet : ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<BodyPartIndex, string> bodyPartLocalizeConfig;

        public string GetText(BodyPartIndex bodyPartIndex)
        {
            return bodyPartLocalizeConfig[bodyPartIndex];
        }
    }
}
