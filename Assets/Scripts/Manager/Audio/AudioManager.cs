using GEvent;
using GRBESystem.UI.Screens.Popups.Setting.General;
using Manager.Resource.Assets;
using TigerForge;
using UnityEngine;

namespace Manager.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private const float MaxMusicVolume = 0.5f;
        private const float MaxVFXVolume = 0.5f;
        
        [SerializeField] private AudioSource backgroundAudioSource;
        [SerializeField] private AudioSource effectAudioSource;
        [SerializeField] private AudioSource actionAudioSource;


        private void Awake()
        {
            EventManager.StartListening(EventName.UI.Select<SoundSettingData>(), OnSetVolume);
            LoadSoundSetting();
            
            EventManager.StartListening(EventName.WidgetEvent.CHANGE_THEME, PlaySoundBackground);
            EventManager.StartListening(EventName.WidgetEvent.PLAY_SOUND_EFFECT, PlaySoundEffect);
            EventManager.StartListening(EventName.WidgetEvent.PLAY_SOUND_ACTION, PlaySoundAction);
        }

        private void LoadSoundSetting()
        {
            var soundSettingData = SoundSettingStorage.LoadLocal();
            EventManager.EmitEventData(EventName.UI.Select<SoundSettingData>(), new SoundSettingData
            {
                isEnableMusic = soundSettingData.haveMusic,
                isEnableSfx = soundSettingData.haveSfx,
                musicVolume = soundSettingData.musicVolume,
                sfxVolume = soundSettingData.sfxVolume,
            });
        }

        private void PlaySound(AudioSource audioSource, AudioClip audioClip, bool isPlayWhenOldClip)
        {
            var oldClip = audioSource.clip;
            if (oldClip == audioClip && isPlayWhenOldClip == false ) return;

            audioSource.Stop();
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        private void PlaySoundBackground()
        {
            var newClip = EventManager.GetData<Theme>(EventName.WidgetEvent.CHANGE_THEME).backgroundSound;
            if(newClip != null) PlaySound(backgroundAudioSource, newClip, false);
        }

        private void PlaySoundAction()
        {
            var newClip = EventManager.GetData<AudioClip>(EventName.WidgetEvent.PLAY_SOUND_ACTION);
            PlaySound(actionAudioSource, newClip, true);
        }
        
        private void PlaySoundEffect()
        {
            var newClip = EventManager.GetData<AudioClip>(EventName.WidgetEvent.PLAY_SOUND_EFFECT);
            PlaySound(effectAudioSource, newClip, true);
        }

        private void OnSetVolume()
        {
            const float minValue = 0f;

            var data = EventManager.GetData(EventName.UI.Select<SoundSettingData>());
            if(data is null) return;

            var soundSettingData = (SoundSettingData) data;
            SaveSoundSetting(soundSettingData);
            
            SetBackgroundVolume(MaxMusicVolume * (soundSettingData.isEnableMusic ? soundSettingData.musicVolume : minValue));
            SetSfxVolume(MaxVFXVolume * (soundSettingData.isEnableSfx ? soundSettingData.sfxVolume : minValue));
        }

        private void SaveSoundSetting(SoundSettingData data)
        {
            var storage = new SoundSettingStorage()
            {
                haveMusic = data.isEnableMusic,
                haveSfx = data.isEnableSfx,
                musicVolume = data.musicVolume,
                sfxVolume =  data.sfxVolume,
            };
            storage.SaveLocal();
        }

        private void SetBackgroundVolume(float volume)
        {
            backgroundAudioSource.volume = volume;
        }

        private void SetSfxVolume(float volume)
        {
            effectAudioSource.volume = volume;
            actionAudioSource.volume = volume;
        }
    }
}