using UnityEngine;

namespace UI.Widget.Toast
{
    [CreateAssetMenu(fileName = "ToastPanelConfig", menuName = "ScriptableObjects/Screen/Toast/ToastConfig")]
    public class ToastPanelConfig : UnityEngine.ScriptableObject
    {
        public float moveDuration;
        public float delayHide;
    }
}