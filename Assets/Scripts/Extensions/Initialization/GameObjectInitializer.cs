using System.Collections.Generic;
using System.Linq;
using GEvent;
using TigerForge;
using UnityEngine;

namespace Extensions.Initialization
{
    public class GameObjectInitializer : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private List<GroupInitializedGameObjects> initializedGameObjectsGroups;
      
        
        private void Awake()
        {
            SetOnlyOnceMainCamera();
            InstantiateGroupsGameObject();
        }

        private void SetOnlyOnceMainCamera()
        {
            EventManager.SetData(EventName.CameraEvent.MAIN_CAMERA, data: mainCamera);
        }

        private void InstantiateGroupsGameObject()
        {
            foreach (var gameObjectsGroup in this.initializedGameObjectsGroups.Where(gameObjectsGroup =>
                gameObjectsGroup.isInstantiateOnAwake))
            {
                gameObjectsGroup.InstantiateAllGroupGameObject(parent: this.transform);
            }
        }
    }
}