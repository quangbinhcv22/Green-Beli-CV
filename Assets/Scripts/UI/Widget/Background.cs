using System;
using GEvent;
using Manager.Resource.Assets;
using TigerForge;
using UnityEngine;

namespace UI.Widget
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer image;

        private void Awake()
        {
            EventManager.StartListening(EventName.WidgetEvent.CHANGE_THEME, ChangeImage);
        }

        private void ChangeImage()
        {
            var background = EventManager.GetData<Theme>(EventName.WidgetEvent.CHANGE_THEME).background;

            if (background == null)
            {
                return;
            }

            image.sprite = background;
        }
    }
}