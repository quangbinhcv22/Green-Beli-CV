using System.Collections.Generic;
using UnityEngine;

namespace Config.Format
{
    [CreateAssetMenu(fileName = "GenerationFormatConfig", menuName = "ScriptableObjects/FormatConfig/Generation")]
    public class GenerationFormatConfig : UnityEngine.ScriptableObject
    {
        private const int NotFindIndex = -1;

        [SerializeField] private List<QB.Collection.KeyValuePair<int, string>> specificPairs;
        [SerializeField] private string other;

        public string GetFormat(int generation)
        {
            var formatIndex = specificPairs.FindIndex(pair => pair.key.Equals(generation));
            var isSpecificFormat = formatIndex > NotFindIndex;

            return isSpecificFormat ? specificPairs[formatIndex].value : other;
        }
    }
}