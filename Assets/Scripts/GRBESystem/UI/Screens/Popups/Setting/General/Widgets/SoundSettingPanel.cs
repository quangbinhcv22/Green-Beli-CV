using GEvent;
using Network.Service;
using TigerForge;
using UI.Widget;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Popups.Setting.General.Widgets
{
    [DefaultExecutionOrder(500)]
    public class SoundSettingPanel : MonoBehaviour
    {
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        
        [SerializeField, Space] private SwitchHandler enableMusicSwitcher;
        [SerializeField] private SwitchHandler enableSfxSwitcher;

        private bool _hasContent;
        

        private void Awake()
        {
            musicVolumeSlider.onValueChanged.AddListener(OnValueChange);
            sfxVolumeSlider.onValueChanged.AddListener(OnValueChange);
            
            enableMusicSwitcher.onValueChanged.AddListener(OnIsOnChange);
            enableSfxSwitcher.onValueChanged.AddListener(OnIsOnChange);
        }

        private void OnEnable()
        {
            if(_hasContent) return;
            
            UpdateView();
            EventManager.StartListening(EventName.UI.Select<SoundSettingData>(), UpdateView);
        }

        private void OnDisable()
        {
            EventManager.StopListening(EventName.UI.Select<SoundSettingData>(), UpdateView);
        }
        
        private void OnIsOnChange(bool isOn) => SendSettingSoundEventData();
        private void OnValueChange(float value) => SendSettingSoundEventData();

        private void UpdateView()
        {
            var data = EventManager.GetData(EventName.UI.Select<SoundSettingData>());
            if(NetworkService.Instance.IsNotLogged() || data is null) return;
            
            _hasContent = true;
            EventManager.StopListening(EventName.UI.Select<SoundSettingData>(), UpdateView);
            
            var settingData = (SoundSettingData) data;
            
            enableMusicSwitcher.IsOn = settingData.isEnableMusic;
            enableSfxSwitcher.IsOn = settingData.isEnableSfx;

            musicVolumeSlider.interactable = settingData.isEnableMusic;
            musicVolumeSlider.value = settingData.musicVolume;

            sfxVolumeSlider.interactable = settingData.isEnableSfx;
            sfxVolumeSlider.value = settingData.sfxVolume;
        }
        
        private void SendSettingSoundEventData()
        {
            EventManager.EmitEventData(EventName.UI.Select<SoundSettingData>(), new SoundSettingData
            {
                isEnableMusic = enableMusicSwitcher.IsOn,
                isEnableSfx = enableSfxSwitcher.IsOn,

                musicVolume = musicVolumeSlider.value,
                sfxVolume = sfxVolumeSlider.value,
            });
        }
    }
}
