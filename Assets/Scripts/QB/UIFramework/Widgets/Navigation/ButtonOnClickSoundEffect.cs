using GEvent;
using TigerForge;
using UI.Widget.ButtonSound;
using UnityEngine;
using Button = UnityEngine.UI.Button;


namespace QB.UIFramework.Widgets.Navigation
{
    public class ButtonOnClickSoundEffect : MonoBehaviour
    {
        [SerializeField] private ButtonSoundEffect buttonSoundEffectConfig;
        [SerializeField] private Button button;

        private void Awake()
        {
            button.onClick.AddListener(PlaySoundEffect);
        }

        public void PlaySoundEffect()
        {
            EventManager.EmitEventData(EventName.WidgetEvent.PLAY_SOUND_EFFECT, buttonSoundEffectConfig.buttonOnClickEffect);
        }
    }
}
