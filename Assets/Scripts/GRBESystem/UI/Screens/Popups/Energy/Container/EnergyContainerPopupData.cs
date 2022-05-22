using GRBESystem.Entity;
using GRBESystem.UI.Screens.Popups.Bridge;
using Manager.Inventory;
using Network.Messages.LoadGame;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Energy.Container
{
    [System.Serializable]
    public struct EnergyContainerPopupData
    {
        public int currentLevel;
        public int nextLevel;
        public int currentCapacity;
        public int nextCapacity;
        public bool isNotCapacityLevelMax;

        
        private const int MinValue = 1;
        
        public static EnergyContainerPopupData Create(EnergyInfo energyInfo, LoadGameConfigResponse loadGameResponse)
        {
            var capacityLevel = energyInfo.Level;
            var capacities = loadGameResponse.energy.capacity;
            var isNotCapacityLevelMax = capacityLevel < capacities.Count;
            
            return new EnergyContainerPopupData()
            {
                currentLevel = capacityLevel,
                nextLevel = isNotCapacityLevelMax ? capacityLevel + MinValue : capacityLevel,
                currentCapacity = capacities[capacityLevel - MinValue].capacity,
                nextCapacity = isNotCapacityLevelMax ? capacities[capacityLevel].capacity : capacities[capacityLevel - 1].capacity,
                isNotCapacityLevelMax = isNotCapacityLevelMax,
            };
        }
    }
}
