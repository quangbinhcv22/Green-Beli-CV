using System;

namespace GRBEGame.UI.Resource.PvpKey
{
    [Serializable]
    public class PvpChest
    {
        public int quantity;
        public QualityChest qualityChest;
    }

    [Serializable]
    public enum QualityChest
    {
        None = 0,
        Silver = 1,
        Gold = 2,
    }
}