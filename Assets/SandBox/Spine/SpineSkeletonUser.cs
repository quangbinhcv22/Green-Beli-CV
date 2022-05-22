using GEvent;
using TigerForge;
using UnityEngine;

namespace SandBox.Spine
{
    public class SpineSkeletonUser : MonoBehaviour
    {
        [SerializeField] private SpineName spineName;
        [SerializeField] private bool isSelectWhenEnabled = true;
        [SerializeField] private bool isUnSelectWhenDisabled = true;


        private void OnEnable()
        {
            if (isSelectWhenEnabled)
                EmitSelectSpineEvent();
        }

        private void OnDisable()
        {
            if (isUnSelectWhenDisabled)
                EmitUnSelectSpineEvent();
        }

        public void EmitSelectSpineEvent()
        {
            EventManager.EmitEventData(EventName.Client.SelectSpine, spineName);
        }
        
        public void EmitUnSelectSpineEvent()
        {
            EventManager.EmitEventData(EventName.Client.UnSelectSpine, spineName);
        }
    }
}
