using UnityEngine;

namespace GRBESystem.UI.Screens.Panels.Disconnect
{
    public class EndlessRotator : MonoBehaviour
    {
        public float speed = -360f;

        void Update()
        {
            transform.Rotate(0f, 0f, speed * Time.deltaTime);
        }
    }
}