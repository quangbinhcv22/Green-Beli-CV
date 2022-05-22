using System.Collections.Generic;
using Localization.Nation;
using UnityEngine;

namespace GRBEGame.UI.Nation
{
    public class SelectNationPanel : MonoBehaviour
    {
        [SerializeField] private NationCellView nationCellViewTemplate;
        [SerializeField] private NationConfig nationConfig;
        [SerializeField] private Transform content;

        private List<NationCellView> _cellViews = new List<NationCellView>();


        private void OnEnable()
        {
            Reload();
        }

        private void Reload()
        {
            var allNations = nationConfig.AllNationIds;
            if (allNations.Count == _cellViews.Count) return;

            foreach (var nation in allNations)
            {
                var newCellView = Instantiate(nationCellViewTemplate, content);
                newCellView.UpdateView(nation);

                _cellViews.Add(newCellView);
            }
        }
    }
}