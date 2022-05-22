using System.Collections.Generic;
using GRBEGame.Define;
using GRBESystem.Entity.Element;
using Network.Service.Implement;

namespace GRBEGame.UI.Screen.Inventory
{
    public static class ItemInventoryDirector
    {
        public static ItemInventory GetItemInventory(ExpCardResponse expCardResponse)
        {
            return new ExpCardItem(expCardResponse);
        }
        
        public static ItemInventory GetItemInventory(FusionStoneResponse fusionStoneResponse)
        {
            return new FusionStoneItem(fusionStoneResponse);
        }
        
        public static ItemInventory GetItemInventory(TrainingHouseResponse trainingHouseResponse)
        {
            return new TrainingHouseItem(trainingHouseResponse);
        }
    }

    [System.Serializable]
    public abstract class ItemInventory
    {
        public string id;
        public int rarity;
        public FragmentType itemInventoryType;
    }

    [System.Serializable]
    public class ExpCardItem : ItemInventory
    {
        public int usedBattles;
        public int maxBattles;
        public int xBooster;
        public int star;


        public ExpCardItem(ExpCardResponse expCardResponse)
        {
            itemInventoryType = FragmentType.ExpCard;
            
            id = expCardResponse.id;
            rarity = expCardResponse.rarity;
            usedBattles = expCardResponse.usedBattles;
            maxBattles = expCardResponse.maxBattles;
            xBooster = expCardResponse.xBooster;
            star = expCardResponse.star;
        }
    }

    [System.Serializable]
    public class FusionStoneItem : ItemInventory
    {
        public int usedPoints;
        public int maxPoints;
        public int star;
        public HeroElement element;


        public FusionStoneItem(FusionStoneResponse fusionStoneResponse)
        {
            itemInventoryType = FragmentType.FusionStone;
            element = (HeroElement) fusionStoneResponse.element;
            
            id = fusionStoneResponse.id;
            rarity = fusionStoneResponse.rarity;
            usedPoints = fusionStoneResponse.usedPoints;
            maxPoints = fusionStoneResponse.maxPoints;
            star = fusionStoneResponse.star;
        }
    }

    [System.Serializable]
    public class TrainingHouseItem : ItemInventory
    {
        public int rooms;
        public List<TrainingHouseRoom> listOfRooms;

       
        public TrainingHouseItem(TrainingHouseResponse trainingHouseResponse)
        {
            itemInventoryType = FragmentType.FusionStone;
            
            id = trainingHouseResponse.id;
            rarity = trainingHouseResponse.rarity;
            rooms = trainingHouseResponse.rooms;
            listOfRooms = trainingHouseResponse.listOfRooms;
        }
    }
}
