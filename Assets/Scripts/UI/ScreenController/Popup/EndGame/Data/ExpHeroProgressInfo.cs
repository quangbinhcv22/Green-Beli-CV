using System;

namespace UI.ScreenController.Popup.EndGame.Data
{
    [Serializable]
    public struct ExpHeroProgressInfo
    {
        public int level;
        public Exp exp;
        public int scoreAdd;

        [Serializable]
        public struct Exp
        {
            public int current;
            public int max;
        }
    }
}