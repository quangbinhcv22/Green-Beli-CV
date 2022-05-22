using System;
using UnityEngine;

namespace GRBESystem.UI.Screens.Popups.Setting.General
{
    [Serializable]
    public class SoundSettingStorage
    {
        private static readonly int DefaultEnable = 1;
        private static readonly float DefaultVolume = 1;

        public bool haveMusic;
        public bool haveSfx;

        public float musicVolume;
        public float sfxVolume;

        public void SaveLocal()
        {
            PlayerPrefs.SetInt(nameof(haveMusic), Convert.ToInt32(haveMusic));
            PlayerPrefs.SetInt(nameof(haveSfx), Convert.ToInt32(haveSfx));

            PlayerPrefs.SetFloat(nameof(musicVolume), musicVolume);
            PlayerPrefs.SetFloat(nameof(sfxVolume), sfxVolume);
                
#if PLATFORM_WEBGL || WEBGLINPUT_TAB || UNITY_WEBGL
            PlayerPrefs.Save();
#endif
        }

        public static SoundSettingStorage LoadLocal()
        {
            var setting = new SoundSettingStorage
            {
                haveMusic = Convert.ToBoolean(PlayerPrefs.GetInt(nameof(haveMusic), DefaultEnable)),
                haveSfx = Convert.ToBoolean(PlayerPrefs.GetInt(nameof(haveSfx), DefaultEnable)),
                musicVolume = PlayerPrefs.GetFloat(nameof(musicVolume), DefaultVolume),
                sfxVolume = PlayerPrefs.GetFloat(nameof(sfxVolume), DefaultVolume)
            };

            return setting;
        }
    }

    [Serializable]
    public class AvatarSettingStorage
    {
        public long avatarID;
        
        
        public void SaveLocal()
        {
            PlayerPrefs.SetString(nameof(avatarID), avatarID.ToString());
            
#if PLATFORM_WEBGL || WEBGLINPUT_TAB || UNITY_WEBGL
            PlayerPrefs.Save();
#endif
        }
        
        public static AvatarSettingStorage LoadLocal()
        {
            var setting = new AvatarSettingStorage
            {
                avatarID = Convert.ToInt64(PlayerPrefs.GetString(nameof(avatarID), "0")),
            };

            return setting;
        }
    }
}