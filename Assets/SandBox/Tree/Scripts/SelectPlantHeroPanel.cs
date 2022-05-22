using System.Collections.Generic;
using System.Linq;
using EnhancedUI.EnhancedScroller;
using GEvent;
using Network.Messages.GetHeroList;
using Network.Service;
using TigerForge;
using UnityEngine;

public class SelectPlantHeroPanel : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private EnhancedScroller scroller;
    [SerializeField] private PlantHeroCellView  heroToSelectSlotPrefab;
    [SerializeField] private GameObject emptyTreeListNote;
    
    private List<HeroResponse> _heroList = new List<HeroResponse>();

    private void OnEnable()
    {
        if (NetworkService.Instance.IsNotLogged()) return;
        ReloadData();
        EventManager.StartListening(EventName.Server.GetListHero, ReloadData);
    }

    private void OnDisable()
    {
        EventManager.StopListening(EventName.Server.GetListHero, ReloadData);
    }
    
    private void ReloadData()
    {
        if (NetworkService.Instance.services.getHeroList.HeroResponses is null) return;
         _heroList = NetworkService.Instance.services.getHeroList.HeroResponses;
        
        emptyTreeListNote.SetActive(_heroList.Any() is false);
        
        ReloadScroller();
    }

    private void ReloadScroller()
    {
        if (scroller.Delegate is null)
        {
            scroller.Delegate = this;
        }
        else
        {
            scroller.ReloadData();
        }
    }
    
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _heroList.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 150f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        var cellView = (PlantHeroCellView) scroller.GetCellView(heroToSelectSlotPrefab);

        cellView.Setup(_heroList[cellIndex].GetID());

        return cellView;
    }
}
