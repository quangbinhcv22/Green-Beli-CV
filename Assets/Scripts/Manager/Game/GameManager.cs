using System;
using System.Globalization;
using Config.Mechanism;
using Config.Other;
using GRBESystem.UI.Screens.Windows.Breeding.Config;
using Pattern;

namespace Manager.Game
{
    public class GameManager : Singleton<GameManager>
    {
        public BreedingConfig breedingConfig;
        public SelectHeroConfig selectHeroConfig;
        public ElementBuffConfig elementBuffConfig;

        private void Awake()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
        }
    }
}