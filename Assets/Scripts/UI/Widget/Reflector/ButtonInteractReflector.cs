using System;
using UI.ArtVisual;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widget.Reflector
{
    /// <summary>
    /// This object reflects its Interact and Art through conditional logic from the outside,
    /// for this object to work properly, you must use SetInteractCondition first,
    /// and then use ReflectInteract every time you review the change.
    /// </summary>
    public class ButtonInteractReflector : MonoBehaviour
    {
        private const string NullInteractConditionMessage = "Must use SetInteractCondition before use ReflectInteract";

        [SerializeField, Space] private UnityEngine.UI.Button button;
        [SerializeField] private Image buttonImage;

        [SerializeField] private ButtonArtSet buttonArtSet;

        private Func<bool> _interactCondition;

        public void SetInteractCondition(Func<bool> interactCondition)
        {
            _interactCondition = interactCondition;
        }

        public void ReflectInteract()
        {
            if (_interactCondition is null) throw new NullInteractConditionException(NullInteractConditionMessage);

            button.interactable = _interactCondition.Invoke();
            if(buttonArtSet) buttonImage.sprite = _interactCondition.Invoke() ? buttonArtSet.normal : buttonArtSet.cantInteract;
        }
    }

    public class NullInteractConditionException : SystemException
    {
        public NullInteractConditionException(string message) : base(message)
        {
        }
    }
}