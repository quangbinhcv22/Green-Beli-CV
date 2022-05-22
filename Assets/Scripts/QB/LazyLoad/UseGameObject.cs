using System;
using UnityEngine;

namespace QB.LazyLoad
{
    public class UseGameObject : MonoBehaviour
    {
        [SerializeField] public LazyLoadReference<GameObject> target;
        [SerializeField] public Transform parent;

        private GameObject _loadedTarget;

        private void OnEnable()
        {
            Use();
        }

        private void OnDisable()
        {
            Recall();
        }

        private void Use()
        {
            if (_loadedTarget)
            {
                _loadedTarget.SetActive(true);
                return;
            }

            if (target.isSet is false) return;

            _loadedTarget = parent is null ? Instantiate(target.asset) : Instantiate(target.asset, parent);
            target = null;
        }

        private void Recall()
        {
            if (_loadedTarget is null) return;

            try
            {
                _loadedTarget.SetActive(false);
            }
            catch (MissingReferenceException)
            {
                //end game
            }
        }
    }
}