using GEvent;
using TigerForge;
using UnityEngine;

namespace GRBESystem.Model.BossModel
{
    public class BossHurtSound : MonoBehaviour
    {
        private const string BOSS_ANIMATION_EVENT = EventName.ScreenEvent.Battle.BOSS_ANIMATION;
        private const string PLAY_SOUND_ACTION_EVENT = EventName.WidgetEvent.PLAY_SOUND_ACTION;
        
        [SerializeField] private AudioClip audioClip;
        

        private void OnEnable()
        {
            EventManager.StartListening(BOSS_ANIMATION_EVENT, EmitAudioEventWhenHurt);
        }

        private void OnDisable()
        {
            EventManager.StopListening(BOSS_ANIMATION_EVENT, EmitAudioEventWhenHurt);
        }

        private void EmitAudioEventWhenHurt()
        {
            if(EventManager.GetData<BossAnimationState>(BOSS_ANIMATION_EVENT) != BossAnimationState.GetHurt) return;
            
            EventManager.EmitEventData(eventName:PLAY_SOUND_ACTION_EVENT, data:audioClip);
        }
    }
}
