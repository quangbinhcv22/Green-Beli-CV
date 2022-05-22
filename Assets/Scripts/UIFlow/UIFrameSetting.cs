using System.Collections.Generic;
using System.Linq;
using UnityEditor;
// using Sirenix.OdinInspector;
using UnityEngine;

namespace UIFlow
{
    [CreateAssetMenu(menuName = "UIFlow/FrameSetting", fileName = nameof(UIFrameSetting))]
    public class UIFrameSetting : ScriptableObject
    {
        // [Searchable]
        // [InfoBox("The order of UIs affect their display layer", InfoMessageType.Info)]
        // [ListDrawerSettings(AddCopiesLastElement = false, NumberOfItemsPerPage = Int32.MaxValue, ShowIndexLabels = true)]
        // [TableList(ShowIndexLabels = true)]

        [SerializeField] private List<UIConfig> uiConfigs;
        [SerializeField] private Canvas sortLayerAsCanvas;

        public UIObject CreateUI(UIId uiId, Transform parent)
        {
            var uiConfigIndex = uiConfigs.FindIndex(uiConfig => uiConfig.id == uiId);
            var isValidQuery = uiConfigIndex >= (int) default;

            if (isValidQuery is false) return null;

            var uiConfig = uiConfigs[uiConfigIndex];
            var uiPrefab = uiConfig.reference.asset;

            var newUi = GameObject.Instantiate(uiPrefab, parent);
            newUi.gameObject.SetActive(false);

            newUi.SetConfig(uiConfig, layer: uiConfigIndex);

            return newUi;
        }

#if UNITY_EDITOR
        public void BuildFrame()
        {
            var canvas = new GameObject("ExampleUIFrame").AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            foreach (var uiConfig in uiConfigs)
            {
                var screenPrefab = uiConfig.reference.asset;
                
                var screen = GameObject.Instantiate(screenPrefab, canvas.transform);
                screen.name = screenPrefab.name;
            }
        }

        public void SortScreenLayerByCanvas()
        {
            var sortedLayerScreens = (from Transform child in sortLayerAsCanvas.transform select child.name).ToList();
            uiConfigs = uiConfigs.OrderBy(uiConfig => sortedLayerScreens.FindIndex(screen => screen == uiConfig.reference.asset.name)).ToList();
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UIFrameSetting))]
    public class UIFrameSettingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (UIFrameSetting) target;

            GUILayout.Space(10);
            if (GUILayout.Button("Build Example Frame", GUILayout.Height(40)))
            {
                script.BuildFrame();
            }
            
            if (GUILayout.Button("Sort layer as Canvas", GUILayout.Height(40)))
            {
                script.SortScreenLayerByCanvas();
            }
        }
    }
#endif
}