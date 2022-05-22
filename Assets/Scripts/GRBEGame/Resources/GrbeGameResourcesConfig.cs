using GRBESystem.Model.HeroIcon;
using UnityEngine;

namespace GRBEGame.Resources
{
    [CreateAssetMenu(fileName = nameof(GrbeGameResourcesConfig), menuName = "ScriptableObject/Resources/Config")]
    public class GrbeGameResourcesConfig : ScriptableObject
    {
        public HeroIconGenerator heroIcon;
        public BodyPartResources bodyPart;
    }
}