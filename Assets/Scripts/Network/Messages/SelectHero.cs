using UnityEngine;

namespace Network.Messages
{
    public class SelectHero : MonoBehaviour
    {
        [System.Serializable]
        public struct SelectHeroRequest
        {
            public long[] heroIds;
            public string gameMode;
        }
    }
}