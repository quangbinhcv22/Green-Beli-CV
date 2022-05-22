using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions.Initialization
{
    [System.Serializable]
    public class GroupInitializedGameObjects
    {
        [SerializeField, Space] private List<GameObject> gameObjectsToInstantiate;

        [Space] public bool isInstantiateOnAwake = true;
        [SerializeField] private bool isActiveAfterInstantiate = false;


        public void InstantiateAllGroupGameObject(Transform parent)
        {
            foreach (var gameObject in
                gameObjectsToInstantiate.Select(gameObject => Object.Instantiate(gameObject, parent)))
            {
                gameObject.SetActive(this.isActiveAfterInstantiate);
            }
        }
    }
}