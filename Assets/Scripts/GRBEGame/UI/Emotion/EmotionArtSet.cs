using System;
using QB.Collection;
using UnityEngine;

namespace GRBEGame.UI.Emotion
{
    [CreateAssetMenu(fileName = nameof(EmotionArtSet), menuName = "ScriptableObject/ArtSet/Emotion")]
    public class EmotionArtSet : ScriptableObject
    {
        public DefaultableDictionary<string, EmotionData> emotions;
        public DefaultableDictionary<EmotionType, Sprite> emotionBackgrounds;
    }

    [Serializable]
    public class EmotionData
    {
        public EmotionType emotionType;
        public Sprite art;

        public bool CanFlipped => emotionType is EmotionType.Sticker;
    }

    public enum EmotionType
    {
        Sticker = 1,
        Text = 2,
    }
}