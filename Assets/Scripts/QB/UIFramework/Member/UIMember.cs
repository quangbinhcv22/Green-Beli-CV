using System;
using UnityEngine;

namespace QuangBinh.UIFramework.Member
{
    [Obsolete]
    public partial class UIMember : MonoBehaviour
    {
        [Header("UI Member")] public Classification classification;
        public LayerConfig layerConfig;
        [SerializeField] private bool isActiveOnAwake;


        private void Awake()
        {
            OtherActionOnAwake();
            gameObject.SetActive(isActiveOnAwake);
        }

        protected virtual void OtherActionOnAwake()
        {
        }
    }

    public partial class UIMember
    {
        public enum Classification
        {
            Window = 0,
            Popup = 100,
            Panel = 200,
            Widget = 300,
            Unknown = 1000,
        }

        [System.Serializable]
        public class LayerConfig
        {
            public Layer layer;
            public int sortOrder;
        }

        public enum Layer
        {
            BelowAll = 0,

            NormalWindow = 100,
            NormalPanel = 110,

            PriorityWindow = 200,
            PriorityPanel = 210,

            NormalPopup = 300,
            PriorityPopup = 310,

            AboveAll = 1000,
        }
    }
}