using UnityEngine;
using UnityEngine.Events;

namespace QB.ViewData
{
    [DefaultExecutionOrder(0)]
    public class DataCoreView : MonoBehaviour
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<object> _onUpdateView;

        public object Data;

        public void AddCallbackUpdate(IDataMemberView memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }

        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(object data)
        {
            Data = data;
            _onUpdateView?.Invoke(data);
        }
    }
}