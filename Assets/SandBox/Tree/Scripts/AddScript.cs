#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AddScript", fileName = nameof(AddScript))]
public class AddScript : ScriptableObject
{
    [SerializeField] private List<GameObject> target;

    public void AddComponent()
    {
    }

    [CustomEditor(typeof(AddScript))]
    public class TestScriptableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (AddScript) target;

            if (GUILayout.Button("Add Script", GUILayout.Height(40)))
            {
                script.AddComponent();
            }
        }
    }
}
#endif