using System.Collections.Generic;
using System.Linq;
using GRBESystem.Entity;
using QuangBinh.UIFramework.Screen;

namespace GRBESystem.UI.Screens.Windows.Loading
{
    [System.Serializable]
    public struct LoadingWindowData
    {
        [System.Serializable]
        public struct LoadProgress
        {
            public float currentPercentage;
        }

        public LoadingWindowRequest loadingRequest;
        public LoadProgress loadingProgress;
    }

    [System.Serializable]
    public struct LoadingWindowRequest
    {
        public ScreenID targetScreenID;
    }
}