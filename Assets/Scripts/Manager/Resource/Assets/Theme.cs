using System.Collections.Generic;
using UnityEngine;

namespace Manager.Resource.Assets
{
    [System.Serializable]
    public class Theme
    {
        public Sprite background;
        public AudioClip backgroundSound;
        public List<DecoGameObjectId> decoIds;
    }
}