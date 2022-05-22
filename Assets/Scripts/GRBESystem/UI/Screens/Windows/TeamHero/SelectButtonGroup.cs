using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    public class SelectButtonGroup : MonoBehaviour
    {
        private const int SimpleCount = 1;

        private List<SelectButton> _selectButtons = new List<SelectButton>();


        public List<SelectButton> SelectButtons => _selectButtons;
        
        public bool HasOnly() => _selectButtons.Count is SimpleCount;
        public bool DontHasOnly() => _selectButtons.Count > SimpleCount;

        public bool IsFirstItem(SelectButton selectButton) => _selectButtons?[default] == selectButton;

        public bool IsExits(SelectButton selectButton) => _selectButtons.Contains(selectButton);

        public bool AnySelected() => _selectButtons.Any(item => item.IsSelected);

        public void AddList(SelectButton selectButton)
        {
            if(_selectButtons.Contains(selectButton)) return;
            _selectButtons.Add(selectButton);

            selectButton.AddOnSelectedCallback(() => InvokeOnclick(selectButton));
        }
        
        public void RemoveList(SelectButton selectButton)
        {
            if(_selectButtons.Contains(selectButton)) return;
            _selectButtons.Remove(selectButton);
            
            if (AnySelected() is false)
                _selectButtons[default].IsSelected = true;
        }

        private void InvokeOnclick(SelectButton selectButton)
        {
            if (selectButton.IsSelected is false) return;
            
            _selectButtons.FindAll(button => button != selectButton).ForEach(button =>
            {
                button.IsSelected = false;
            });
        }

        private void Awake()
        {
            if(DontHasOnly() && AnySelected() is false)
                _selectButtons[default].Set(true);
        }

        private void OnValidate()
        {
            if (DontHasOnly() && AnySelected() is false)
                _selectButtons[default].Set(true);
        }
    }
}
