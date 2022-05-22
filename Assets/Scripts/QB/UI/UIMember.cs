using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace QB.UI
{
    public class UIMember : MonoBehaviour
    {
        public enum UIMemberDefaultType
        {
            Window = 0,
            Panel = 1,
            Popup = 2,
            Widget = 3,
        }

        [SerializeField] private UIMemberDefaultType type;
        [HideInInspector] [SerializeField] private int idIndex;

        public int IDIndex
        {
            get => idIndex;
            set
            {
                if (idIndex.Equals(value)) return;
                idIndex = value;
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }

        public UIMemberDefaultType Type => type;
        

        public void Open()
        {
            SetActive(true);
        }

        public void Close()
        {
            SetActive(false);
        }

        public void Recall()
        {
            Destroy(this);
        }

        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UIMember))]
    public class UIMemberEditor : Editor
    {
        private string[] _screenIdNames;
        private int _selectIndex;

        private int TargetScreenIdIndex => ((UIMember)target).IDIndex;

        private void OnEnable()
        {
            _screenIdNames = ScreenDefine.ScreenIds.ToArray();

            var targetScreenIdName = Enum.GetName(ScreenDefine.ScreenEnumType, TargetScreenIdIndex) ?? string.Empty;
            _selectIndex = _screenIdNames.ToList().FindIndex(idName => idName == targetScreenIdName);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _selectIndex = EditorGUILayout.Popup($"ID (Index: {TargetScreenIdIndex})", _selectIndex, _screenIdNames);

            var screen = (UIMember)target;
            if (_selectIndex < (int)default) return;

            var screenIndex = (int)Enum.Parse(ScreenDefine.ScreenEnumType, _screenIdNames[_selectIndex], true);
            screen.IDIndex = screenIndex;
        }
    }
#endif
}