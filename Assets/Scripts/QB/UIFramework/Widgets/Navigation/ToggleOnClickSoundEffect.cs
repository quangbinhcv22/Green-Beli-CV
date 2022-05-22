using GEvent;
using TigerForge;
using UI.Widget.ButtonSound;
using UnityEngine;
using UnityEngine.UI;


namespace QuangBinh.UIFramework.Widgets.Navigation
{
    public class ToggleOnClickSoundEffect : MonoBehaviour
    {
        [SerializeField] private ButtonSoundEffect buttonSoundEffectConfig;
        [SerializeField] private Toggle toggle;

        private void Awake()
        {
            toggle.onValueChanged.AddListener(PlaySoundEffect);
        }

        private void PlaySoundEffect(bool enable)
        {
            EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_EFFECT, buttonSoundEffectConfig.buttonOnClickEffect);
        }
    }
}
