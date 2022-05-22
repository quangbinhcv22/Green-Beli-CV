using System;
using System.Collections.Generic;
using GRBESystem.Definitions.BodyPart.DataGroup;
using UnityEngine;

namespace GRBESystem.Model.HeroIcon
{
    public class HeroIconGenerator : MonoBehaviour
    {
        private static readonly Dictionary<string, Sprite> CharacterIcons = new Dictionary<string, Sprite>();

        [SerializeField] private new Camera camera;
        [SerializeField] private int iconSize = 256;
        [SerializeField] private BodyPartsGroup bodyParts;


        public Sprite GetIcon(string heroId)
        {
            return CharacterIcons.ContainsKey(heroId) ? CharacterIcons[heroId] : CreateIcon(heroId);
        }

        private Sprite CreateIcon(string heroId)
        {
            bodyParts.ChangeSkin(heroId);
            CharacterIcons.Add(heroId, CamCapture(spriteName: heroId));

            return CharacterIcons[heroId];
        }

        private Sprite CamCapture(string spriteName)
        {
            var currentRt = RenderTexture.active;
            RenderTexture.active = camera.targetTexture;

            camera.Render();

            var targetTexture = camera.targetTexture;
            var image = new Texture2D(targetTexture.width, targetTexture.height);
            image.ReadPixels(new Rect(default, default, targetTexture.width, targetTexture.height), default, default);
            image.Apply();
            RenderTexture.active = currentRt;

            var characterSprite = Sprite.Create(image, new Rect(default, default, iconSize, iconSize), Vector2.zero);
            characterSprite.name = spriteName;

            return characterSprite;
        }
    }
}