using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIFlow.InGame
{
    public class RequestUICollider : MonoBehaviour
    {
        public bool canAccess = true;
        public UIRequest request;

        private void OnMouseUpAsButton()
        {
            if (canAccess is false) return;
            if (IsIgnoreRaycast()) request.SendRequest();
        }

        private static bool IsIgnoreRaycast()
        {
            const string ignoreRaycastTag = "IgnoreRaycast";

            var eventData = new PointerEventData(EventSystem.current) {position = Input.mousePosition};

            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            return raycastResults.All(IsIgnoreRaycastObject);


            bool IsIgnoreRaycastObject(RaycastResult result) => result.gameObject.CompareTag(ignoreRaycastTag);
        }
    }
}