using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GRBESystem.UI.Screens.Windows.MainHall.Widgets.PlayerInfoPanel
{
    public abstract class MoneyBar : MonoBehaviour
    {
        private TextMeshProUGUI _textValue;

        private Button _buttonAddMoney;
        // Start is called before the first frame update
        private void Awake()
        {
            _textValue = GetComponentInChildren<TextMeshProUGUI>(true);

            _buttonAddMoney = GetComponentInChildren<Button>(true);
        }

        protected virtual void Start()
        {
            _buttonAddMoney.onClick.AddListener(OnClick);
        }

        protected void SetMoney(int value)
        {
            _textValue.text = value.ToString();
        }

        protected abstract void OnClick();
    }
}
