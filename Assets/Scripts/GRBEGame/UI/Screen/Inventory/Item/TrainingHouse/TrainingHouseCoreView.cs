using UnityEngine;
using UnityEngine.Events;


namespace GRBEGame.UI.Screen.Inventory
{
    public class TrainingHouseCoreView : MonoBehaviour, ICoreView<TrainingHouseItem>
    {
        private UnityAction _onUpdateDefault;
        private UnityAction<TrainingHouseItem> _onUpdateView;


        public void UpdateDefault()
        {
            _onUpdateDefault?.Invoke();
        }

        public void UpdateView(TrainingHouseItem data)
        {
            _onUpdateView?.Invoke(data);
        }

        public void AddCallBackUpdateView(IMemberView<TrainingHouseItem> memberView)
        {
            _onUpdateDefault += memberView.UpdateDefault;
            _onUpdateView += memberView.UpdateView;
        }
    }
}
