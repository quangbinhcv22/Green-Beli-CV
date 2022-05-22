using Spine.Unity;
using UnityEngine;

namespace GRBESystem.Model.OptimizeHeroModel.Pool
{
    public class OptimizedSkeletonAnimationModel : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private SkeletonAnimation _skeletonAnimation;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void OnEnable()
        {
            _meshRenderer.enabled = true;
            _skeletonAnimation.enabled = true;
        }

        private void OnDisable()
        {
            _meshRenderer.enabled = false;
            _skeletonAnimation.enabled = false;
        }
    }
}