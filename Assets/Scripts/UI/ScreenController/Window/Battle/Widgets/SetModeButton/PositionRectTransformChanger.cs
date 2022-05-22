using System;
using UnityEngine;

namespace UI.ScreenController.Window.Battle.Widgets.SetModeButton
{
    public class PositionRectTransformChanger : MonoBehaviour
    {
        [SerializeField] private RectTransform target;
        [SerializeField] private Vector3 changePosition;
        [SerializeField] private Vector3 resetPosition;


        public void ChangePosition()
        {
            target.localPosition = changePosition;
        }

        public void ReSetPosition()
        {
            target.localPosition = resetPosition;
        }
    }
}