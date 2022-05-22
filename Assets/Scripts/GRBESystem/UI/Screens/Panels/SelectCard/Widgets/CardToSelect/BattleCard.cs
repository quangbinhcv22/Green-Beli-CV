using System;
using DG.Tweening;
using GEvent;
using TigerForge;
using UI.ScreenController.Panel.SelectCard.Selector;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Panels.SelectCard.Widgets.CardToSelect
{
    [RequireComponent(typeof(Button))]
    public class BattleCard : MonoBehaviour
    {
        [SerializeField, Space] private Image image;
        [SerializeField] private CardIndexConfig artSet;
        [SerializeField] private Image outLine;

        private Button _button;
        private int _cardIndex;


        [Header("Animation")] [SerializeField, Space]
        private float scaleOnSelected = 1.05f;

        [SerializeField] private float scaleNormal = 1f;
        [SerializeField] private float scaleDuration = 0.3f;
        [SerializeField] private Ease scaleEase = Ease.Linear;

        [SerializeField, Space] private float positionYOnSelected;
        [SerializeField] private float moveYDuration = 0.3f;
        [SerializeField] private Ease moveEase = Ease.Linear;
        private float _positionYNormal;


        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SelectCard);

            EventManager.StartListening(EventName.Select.BattleCard, OnSelectCard);

            _positionYNormal = transform.localScale.y;
        }

        private void OnDisable()
        {
            PerformAnimation(false);
        }

        public void SetIndex(int index)
        {
            _cardIndex = index;

            UpdateView();
            OnSelectCard();
        }

        private void SelectCard()
        {
            EventManager.EmitEventData(EventName.Select.BattleCard, _cardIndex);
        }

        private void UpdateView()
        {
            image.sprite = artSet.GetCardSprite(_cardIndex);
        }

        private void OnSelectCard()
        {
            var isSelected = _cardIndex == SelectedIndex();

            _button.enabled = isSelected is false;
            PerformAnimation(isSelected);
            outLine.gameObject.SetActive(isSelected);
        }

        private static int SelectedIndex()
        {
            var nullableIndex = EventManager.GetData(EventName.Select.BattleCard);

            if (nullableIndex is int index) return index;
            return default;
        }

        private void PerformAnimation(bool isSelected)
        {
            // animation - tai cau truc sau
            var targetScale = Vector3.one * (isSelected ? scaleOnSelected : scaleNormal);
            transform.DOScale(targetScale, scaleDuration).SetEase(scaleEase);

            var targetPosition = isSelected ? positionYOnSelected : _positionYNormal;
            transform.DOLocalMoveY(targetPosition, moveYDuration).SetEase(moveEase);
        }
    }
}