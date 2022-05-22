using System.Collections.Generic;
using System.Linq;
using GRBEGame.Resources;
using GRBESystem.Definitions.BodyPart.Index;
using UnityEngine;

namespace GRBESystem.Definitions.BodyPart.DataGroup
{
    [System.Serializable]
    public class BodyPartsGroup
    {
        public List<QB.Collection.KeyValuePair<BodyPartIndex, SpriteRenderer>> partRenders;

        public List<SpriteRenderer> Renderers => partRenders.Select(pair => pair.value).ToList();


        public void ChangeSkin(string heroId)
        {
            partRenders.ForEach(pair => pair.value.sprite = GetBodyPartSprite(pair.key, heroId));
        }

        private static Sprite GetBodyPartSprite(BodyPartIndex partIndex, string heroId)
        {
            var partInfo = new BodyPartInfo((PartID)partIndex, heroId);
            return GrbeGameResources.Instance.BodyPartSprites.GetSprite(partInfo);
        }
    }
}