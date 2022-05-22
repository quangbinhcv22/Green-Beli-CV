using UnityEngine;

namespace UI.Widget.ButtonSound
{
    [CreateAssetMenu(fileName = "ButtonSoundSet", menuName = "ScriptableObjects/SoundSet/Button")]
    public class ButtonSoundEffect : UnityEngine.ScriptableObject
    {
        public AudioClip buttonOnClickEffect;
    }
}
