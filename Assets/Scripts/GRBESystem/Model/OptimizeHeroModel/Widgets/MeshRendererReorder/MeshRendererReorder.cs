using UnityEngine;

namespace GRBESystem.Model.OptimizeHeroModel.Widgets.MeshRendererReorder
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshRendererReorder : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private bool isReorderOnDisable = true;

        private int _olOrder;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            _olOrder = meshRenderer.sortingOrder;
        }

        private void OnDisable()
        {
            if (isReorderOnDisable == false) return;
            Reorder(newOrder: _olOrder);
        }

        private void Reorder(int newOrder)
        {
            meshRenderer.sortingOrder = newOrder;
        }

        public void AddOrder(int orderAdd)
        {
            Reorder(_olOrder + orderAdd);
        }
    }
}