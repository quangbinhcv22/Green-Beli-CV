using GRBESystem.Model.HeroIcon;
using Pattern;
using UnityEngine;

namespace GRBEGame.Resources
{
    public class GrbeGameResources : Singleton<GrbeGameResources>
    {
        [SerializeField] private GrbeGameResourcesConfig resourcesConfig;


        private HeroIconGenerator _heroIcon;
        public HeroIconGenerator HeroIcon => _heroIcon ? _heroIcon : (_heroIcon = Instantiate(resourcesConfig.heroIcon, transform));


        public BodyPartResources BodyPartSprites => resourcesConfig.bodyPart;
    }
}