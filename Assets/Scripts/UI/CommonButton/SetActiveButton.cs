using System;
using UIFlow;
using UIFlow.InGame;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CommonButton
{
    [RequireComponent(typeof(Button))]
    public class SetActiveButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject gameObjectSetActive;
        [SerializeField] private bool isActive;

        private void Awake()
        {
            button.onClick.AddListener(() => gameObjectSetActive.SetActive(isActive));
        }

        private void Reset()
        {
            button = GetComponent<Button>();
        }

        private void OnValidate()
        {
            // if (gameObjectSetActive is null)
            // {
            //     Debug.Log($"Hi, I'm {name}, I in <color=yellow>{gameObjectSetActive.name}</color>. Target is null");
            //     return;
            // }
            //
            // if (gameObjectSetActive.GetComponent<UIObject>())
            // {
            //
            //
            //     if (gameObject.GetComponent<ActiveUIButton>() is null)
            //     {
            //         var activeUIButton = gameObject.AddComponent<ActiveUIButton>();
            //
            //         activeUIButton.ui = gameObjectSetActive.GetComponent<UIObject>();
            //         activeUIButton.isActive = isActive;
            //     }
            //
            //     Debug.Log(
            //         $"Hi, I'm {name}, I in <color=yellow>{gameObjectSetActive.name}</color>. I'm Obsolete, please delete me and replace with <color=yellow>ActiveUIButton</color>",
            //         gameObject);            }
        }
    }
}