using GRBESystem.Definitions.BodyPart.Index;
using QB.Collection;
using UnityEngine;

namespace Config.Format
{
    [CreateAssetMenu(fileName = nameof(BodyPartFormatSet), menuName = "ScriptableObject/FormatSet/BodyPart")]
    public class BodyPartFormatSet : ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<BodyPartIndex, string> bodyPartFormatConfig;

        public string GetFormat(BodyPartIndex bodyPartIndex)
        {
            return bodyPartFormatConfig[bodyPartIndex];
        }
    }
}
