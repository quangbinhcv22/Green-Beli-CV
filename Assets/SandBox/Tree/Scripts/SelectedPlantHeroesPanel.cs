using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using GEvent;
using TigerForge;
using UnityEngine;

public class SelectedPlantHeroesPanel : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private EnhancedScroller scroller;
    [SerializeField] private PlantHeroCellView cellViewPrefab;
    
    private long _selectedHeroes;
    
    private void OnEnable()
    {
        ReloadData();
        EventManager.StartListening(EventName.Select.PlantHero, ReloadData);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Select.PlantHero, ReloadData);
    }
    
    private void ReloadData()
    {
        var nullableSelectedHeroes = EventManager.GetData(EventName.Select.PlantHero);
        
        if (nullableSelectedHeroes is long heroId) _selectedHeroes = heroId;
        scroller.Delegate = this;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return 1;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 130f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        var cellView = (PlantHeroCellView) scroller.GetCellView(cellViewPrefab);

        cellView.Setup(_selectedHeroes);
       
        return cellView;
    }
}
