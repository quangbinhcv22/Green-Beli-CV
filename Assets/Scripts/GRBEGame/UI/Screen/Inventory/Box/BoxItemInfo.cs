using Network.Service.Implement;

namespace GRBEGame.UI.Screen.Inventory
{
    [System.Serializable]
    public class BoxItemInfo
    {
        public int type;
        public int count;
        public long id;
        public BoxItemType boxItemType;

        public BoxItemInfo(int type, int count)
        {
            this.type = type;
            this.count = count;
        }

        public BoxItemInfo(BoxResponse boxResponse)
        {
            boxItemType = BoxItemType.Box;
            type = boxResponse.type;
            id = boxResponse.id;
        }
        
        public BoxItemInfo(PackResponse packResponse)
        {
            boxItemType = BoxItemType.Pack;
            count = packResponse.number;
            type = packResponse.type;
            id = packResponse.id;
        }
    }
}
