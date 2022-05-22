using System.Collections;
using DG.Tweening;
using GEvent;
using TigerForge;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ScreenController.Panel.SelectCard.Selector
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {
        private Image _image;
        public int selectIndex;

        [SerializeField] private Sprite faceSprite, backSprite;
        [HideInInspector] public UnityEvent onClick;


        public void SetSelectIndex(int newIndex)
        {
            selectIndex = newIndex;
        }

        private void EmmitEventSelectIndex()
        {
            EventManager.EmitEventData(EventName.ScreenEvent.Battle.SELECTING_CARD, data: selectIndex);
        }


        private void Awake()
        {
            onClick.AddListener(EmmitEventSelectIndex);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke();
        }

        
        public void FlipOpenTo(Sprite sprite)
        {
            this._image = GetComponent<Image>();
            _image.sprite = sprite;
            
            //StartCoroutine(RotateFlip((sprite)));
        }

        private IEnumerator RotateFlip(Sprite sprite)
        {
            this._image = GetComponent<Image>();
            this._image.sprite = this.backSprite;
            this.transform.localScale = new Vector3(-1, 1, 1);

            var targetVector = new Vector3(1, 1, 1);
            var targetSprite = sprite;

            this.transform.DOScale(targetVector, 0.5f).SetEase(Ease.Linear);

            yield return new WaitForSeconds(0.25f);
            this._image.sprite = sprite;
        }
    }
}