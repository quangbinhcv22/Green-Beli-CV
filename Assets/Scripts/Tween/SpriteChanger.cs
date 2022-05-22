using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Tween
{
    public class SpriteChanger : MonoBehaviour
    {
        [SerializeField] [Space] private Image image;
        [SerializeField] private Sprite spriteTarget;

        private void Awake()
        {
            Assert.IsNotNull(image);
        }

        public void Change()
        {
            image.sprite = spriteTarget;
        }
    }
}