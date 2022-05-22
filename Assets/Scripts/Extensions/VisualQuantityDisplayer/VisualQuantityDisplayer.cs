using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions.VisualQuantityDisplayer
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class VisualQuantityDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject contentPrefab;
        [SerializeField] private Vector2 sizeContent = new Vector2(20, 20);

        private readonly List<GameObject> _contentPrefabInstantiates = new List<GameObject>();


        public void VisualDisplay(int targetNumber)
        {
            InitializeMoreIfMissing(targetNumber);
            
            for (var i = 0; i < this._contentPrefabInstantiates.Count; i++)
            {
                var isActive = i < targetNumber;
                this._contentPrefabInstantiates[i].SetActive(isActive);
            }
        }

        private void InitializeMoreIfMissing(int targetNumber)
        {
            for (var i = this._contentPrefabInstantiates.Count; i < targetNumber; i++)
            {
                this._contentPrefabInstantiates.Add(Instantiate(contentPrefab, parent: this.transform));
                _contentPrefabInstantiates[i].GetComponent<RectTransform>().sizeDelta = sizeContent;
            }
        }
    }
}