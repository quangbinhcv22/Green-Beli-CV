using System;
using UnityEngine;

namespace Extensions.Initialization.Request
{
    [System.Serializable]
    public struct ShowHeroModelRequest
    {
        [NonSerialized] public long heroId;
        public Vector3 position;
        public Vector3 scale;
        public bool isFlip;
        public int addOrderInLayer;
    }
}