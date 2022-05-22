using System.Collections.Generic;
using UnityEngine;

namespace Extensions.UXGameObject.Motion
{
    public class MotionGameObjectsHandler : MonoBehaviour
    {
        [SerializeField] private List<MotionGameObject> motionGameObjects;

        public void ShowMotionGameObjects()
        {
            foreach (var motionGameObject in motionGameObjects)
            {
                motionGameObject.MoveToShow();
            }
        }

        public void HideMotionGameObjects()
        {
            foreach (var motionGameObject in motionGameObjects)
            {
                motionGameObject.MoveToHide();
            }
        }
    }
}