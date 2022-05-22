using System;
using TMPro;
using UnityEngine;

namespace UI.Widget.Time
{
    [RequireComponent(typeof(TMP_Text))]
    public class NowDateTimeText : MonoBehaviour
    {
        private enum TimeType
        {
            Local = 0,
            Utc = 1,
        }

        [SerializeField] private TimeType timeType = TimeType.Local;
        [SerializeField] private string dateFormat = "dd/MM/yyyy";

        private TMP_Text _dateText;

        private void Awake()
        {
            _dateText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _dateText.SetText(GetNowDateTime().ToString(dateFormat));
        }

        private DateTime GetNowDateTime()
        {
            return timeType switch
            {
                TimeType.Local => DateTime.Now,
                TimeType.Utc => DateTime.UtcNow,
                _ => DateTime.Now
            };
        }
    }
}