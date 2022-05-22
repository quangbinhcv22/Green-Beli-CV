using GRBEGame.Define;
using Network.Service.Implement;

namespace GRBEGame.UI.Screen.Inventory.Material
{
    [System.Serializable]
    public class MaterialInfo
    {
        public int type;
        public int count;
        public MaterialType materialType;

        public MaterialInfo(int type, int count)
        {
            this.type = type;
            this.count = count;
        }

        public MaterialInfo(MaterialResponse materialResponse)
        {
            materialType = (MaterialType) materialResponse.type;
            type = materialResponse.type;
            count = materialResponse.number;
        }
    }
}
