using UnityEngine;

namespace GRBESystem.Model.OptimizeHeroModel.Widgets.Rescale
{
    public class Rescaler : MonoBehaviour
    {
        private Vector3 _oldScale;

        private void Awake()
        {
            _oldScale = transform.localScale;
        }
        
        private void OnEnable()
        {
            Rescale(_oldScale);
        }
        
        private void OnDisable()
        {
            Rescale(_oldScale);
        }
        
        private void Rescale(Vector3 newScale)
        {
            transform.localScale = newScale;
        }
    }
}