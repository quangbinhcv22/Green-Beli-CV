using UnityEngine;

namespace GScreen
{
    // tai cau truc sau

    [CreateAssetMenu(menuName = "UI/Loading", fileName = nameof(LoadingConfig))]
    public class LoadingConfig : ScriptableObject
    {
        [SerializeField] private float mainHallDelay = 5f;
        [SerializeField] private float coverDelay = 1f;

        public float GetMainHallDelay()
        {
#if UNITY_EDITOR
            return 0.1f; //fast test
#endif

            return mainHallDelay;
        }

        public float GetAnimationCoverDelay()
        {
            return coverDelay;
        }

        public float GetFinalDelay()
        {
#if UNITY_EDITOR
            return 0.1f; //fast test
#endif

            return mainHallDelay + coverDelay;
        }
    }
}