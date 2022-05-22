using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.TeamHero
{
    [RequireComponent(typeof(Button))]
    public class SelectButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SelectButtonGroup group;
        [SerializeField] private bool isSelect;
        [SerializeField] [Space] private Sprite selectSprite;
        [SerializeField] private Sprite unselectSprite;
        [SerializeField] [Space] private UnityEvent selectCallback;
        [SerializeField] private UnityEvent unSelectCallback;
        
        private bool _isSelectTemp;
        private UnityAction _onSelected;
        private UnityAction _onUnSelected;


        public bool IsSelected
        {
            get => isSelect;
            set => Set(value);
        }

        public void Set(bool value)
        {
            isSelect = value;
            _isSelectTemp = isSelect;
            button.image.sprite = value ? selectSprite : unselectSprite;
            (value ? _onSelected : _onUnSelected)?.Invoke();
            (value ? selectCallback : unSelectCallback)?.Invoke();
        }

        public void AddOnSelectedCallback(UnityAction callback) => _onSelected += callback;
        
        public void AddOnUnSelectedCallback(UnityAction callback) => _onUnSelected += callback;
        
        private void Awake()
        {
            button.onClick.AddListener(OnChangeState);
        }

        private void OnEnable()
        {
            if (group != null && group.IsExits(this) is false)
                AddGroup();
        }

        private void OnDisable()
        {
            if (group != null && group.IsExits(this))
                RemoveFormGroup();
        }

        private void OnMouseDown()
        {
            OnChangeState();
        }

        private void OnChangeState()
        {
            if (IsSelected is false)
                IsSelected = true;
            else
            {
                if (group != null && group.DontHasOnly()) return;
                IsSelected = false;
            }
        }

        private void AddGroup()
        {
            if(group.IsExits(this)) return;

            group.AddList(this);
            if (group.IsFirstItem(this) is false && group.AnySelected())
                IsSelected = false;
        }
        
        private void RemoveFormGroup()
        {
            if(group.IsExits(this)) return;
            group.RemoveList(this);
        }
        
        private void OnValidate()
        {
            button = GetComponent<Button>();
            
            if (group != null)
                AddGroup();
            UpdateStateOnValidate();
        }

        private void UpdateStateOnValidate()
        {
            if (_isSelectTemp == isSelect)
                return;
            
            if (group != null && group.DontHasOnly())
            { 
                if (isSelect is false) 
                { 
                    Set(true); 
                    return;
                }
                
                Set(isSelect); 
                group.SelectButtons.ForEach(item => {
                    if (item != this && item != null) 
                        item.Set(isSelect is false);
                });
            }
            else 
                Set(isSelect);
        }
        
    }
}
