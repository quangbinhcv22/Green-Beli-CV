using GEvent;
using Spine.Unity;
using TigerForge;
using UnityEngine;

namespace SandBox.Spine
{
    public class SpineSkeletonAnimationObject : MonoBehaviour
    {
        [SerializeField] private SpineObjectData spineObjectData;


        private void Awake()
        {
            spineObjectData.SetActive(default);
            EventManager.StartListening(EventName.Client.SelectSpine, SelectSpine);
            EventManager.StartListening(EventName.Client.UnSelectSpine, UnSelectSpine);
        }

        private void SelectSpine()
        {
            var data = EventManager.GetData(EventName.Client.SelectSpine);
            if(data is null || (SpineName) data != spineObjectData.Name) 
                return;

            spineObjectData.SetActive(true);
        }

        private void UnSelectSpine()
        {
            var data = EventManager.GetData(EventName.Client.UnSelectSpine);
            if(data is null || (SpineName) data != spineObjectData.Name) 
                return;

            spineObjectData.SetActive(default);
        }
    }

    [System.Serializable]
    public class SpineObjectData
    {
        [SerializeField] private SpineName spineName;
        [SerializeField] private SkeletonAnimation skeletonAnimation;


        public SpineName Name => spineName;
        
        public void SetActive(bool isActive)
        {
            skeletonAnimation.gameObject.SetActive(isActive);
        }
    }

    [System.Serializable]
    public enum SpineName
    {
        FireflyBackground = 0,
    }
}
