using QB.Collection;
using UnityEngine;


namespace UI.ArtVisual
{
    [CreateAssetMenu(fileName = "LuckyGreenbieRankArtSet", menuName = "ScriptableObjects/ArtSet/LuckyGreenbieRank")]
    public class LuckyGreenbieRankArtSet : UnityEngine.ScriptableObject
    {
        [SerializeField] private DefaultableDictionary<int, Sprite> rankArtConfigs;

        public Sprite GetRankIcon(int rank)
        {
            return rankArtConfigs[rank];
        }

        public int GetMaxCount()
        {
            return rankArtConfigs.customPairs.Count;
        }
    }
}