using System.Collections.Generic;
using System.Linq;
using Log;
using TMPro;
using UnityEngine;

namespace QB.ViewData
{
    [DefaultExecutionOrder(-200)]
    [RequireComponent(typeof(TMP_Text))]
    public class DataTextView : MonoBehaviour, IDataMemberView
    {
        [SerializeField] [Space] private DataCoreView coreView;
        [SerializeField] private List<string> memberNames;

        [SerializeField] [Space] [TextArea] private string format = "{0}";
        [SerializeField] [TextArea] private string textDefault;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            coreView.AddCallbackUpdate(this);
        }

        public void UpdateDefault()
        {
            _text.SetText(textDefault);
        }

        public void UpdateView(object data)
        {
            try
            {
                _text.SetText(string.Format(format, memberNames.Select(data.Get).ToArray()));
            }
            catch (UnableAccessDataViewException e)
            {
                GLogger.LogError(e.Message);
                UpdateDefault();
            }
        }

        private void OnValidate()
        {
            if (coreView is null)
            {
                Debug.Log($"<color=yellow>{nameof(coreView)}</color> is null", gameObject);
            }
        }
    }
}