using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace VFX.ChestVfx
{
    public class OpenChestVFXHandler : MonoBehaviour
    {
        [SerializeField] private PlayableDirector idleDirector;
        [SerializeField] private PlayableDirector actionDirector;
        [SerializeField] private UnityEvent idleActiveObject;
        [SerializeField] private UnityEvent actionActiveObject;
        [SerializeField] private Button openButton;
        [SerializeField] private Button okButton;


        private void Awake()
        {
            openButton.onClick.AddListener(ChangeState);
        }

        private void OnEnable()
        {
            idleDirector.Play();
            idleActiveObject?.Invoke();
            
            actionDirector.Stop();
        }

        private void OnDisable()
        {
            okButton.transform.DOScale(Vector3.zero, 0.5f);
            okButton.DOKill();
        }

        private void ChangeState()
        {
            idleDirector.Stop();
            actionDirector.Play();
            actionActiveObject?.Invoke();

            StartCoroutine(ButtonAmin());
        }

        private IEnumerator ButtonAmin()
        {
            yield return new WaitForSeconds(5f);
            okButton.gameObject.transform.DOScale(Vector3.one, 0.5f);
        }
    }
}
