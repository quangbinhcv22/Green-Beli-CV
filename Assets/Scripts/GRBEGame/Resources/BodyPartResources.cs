using System;
using System.Collections.Generic;
using System.Text;
using GRBEGame.ArtSet;
using UnityEngine;

namespace GRBEGame.Resources
{
    public enum PartID
    {
        Face = 1,
        Hair = 2,
        Body = 3,
        FrontHand = 4,
        BackHand = -4,
        Leg = 5,
        Deco = 6,
    }

    public enum Element
    {
        Metal = 1,
        Wood = 2,
        Water = 3,
        Earth = 4,
        Fire = 5,
    }

    public enum Version
    {
        Ver1 = 1,
        Ver2 = 2,
    }

    public enum Side
    {
        None = -1,
        Back = 0,
        Front = 1,
    }

    [Serializable]
    public class BodyPartInfo
    {
        private static readonly List<PartID> PartsHaveSide = new List<PartID> { PartID.FrontHand, PartID.BackHand };

        public PartID partID;
        public Element element;
        public Version version;

        public BodyPartInfo()
        {
        }

        public BodyPartInfo(PartID partID, string heroId)
        {
            const int lengthBodyPartIDInHeroID = 2;

            int startIndex = Mathf.Abs(((int)partID - 1) * lengthBodyPartIDInHeroID);
            var partId = $"{heroId.Substring(startIndex, lengthBodyPartIDInHeroID)}";

            element = (Element)int.Parse(partId[0].ToString());
            version = (Version)int.Parse(partId[1].ToString());

            this.partID = partID;
        }

        private Side GetSide()
        {
            return HaveSide() ? RealSide() : Side.None;

            bool HaveSide() => PartsHaveSide.Contains(partID);
            Side RealSide() => (int)partID < (int)default ? Side.Front : Side.Back;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Mathf.Abs((int)partID)).Append((int)element).Append((int)version);
            if (GetSide() != Side.None) stringBuilder.Append('_').Append((int)GetSide());

            return stringBuilder.ToString();
        }
    }

    [Serializable]
    public class BodyPartResources
    {
        private readonly Dictionary<string, Sprite> _spriteDic = new Dictionary<string, Sprite>();

        [SerializeField] private BodyPartArtSet bodyPartArtSet;


        public Sprite GetSprite(BodyPartInfo bodyPartInfo)
        {
            return GetSprite(bodyPartInfo.ToString());
        }

        private Sprite GetSprite(string fileName)
        {
            if (_spriteDic.ContainsKey(fileName)) return _spriteDic[fileName];
            _spriteDic.Add(fileName, bodyPartArtSet.GetArt(fileName));

            return GetSprite(fileName);
        }
    }
}