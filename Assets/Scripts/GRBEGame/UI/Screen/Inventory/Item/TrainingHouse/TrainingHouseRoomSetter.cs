using System.Collections.Generic;
using GRBEGame.UI.Screen.Inventory;
using Network.Service.Implement;
using UnityEngine;

public class TrainingHouseRoomSetter : MonoBehaviour, IMemberView<TrainingHouseItem>
{
    [SerializeField] private TrainingHouseCoreView coreView;
    [SerializeField] private TrainingHouseRoomView roomPrefab;
    [SerializeField] private Transform parent;

    private List<TrainingHouseRoomView> _trainingHouseRoomViews;


    private void Awake()
    {
        _trainingHouseRoomViews = new List<TrainingHouseRoomView>();
        coreView.AddCallBackUpdateView(this);
    }

    public void UpdateDefault()
    {
        UpdateRoomView(new List<TrainingHouseRoom>());
    }

    public void UpdateView(TrainingHouseItem data)
    {
        UpdateRoomView(data.listOfRooms);
    }

    private void UpdateRoomView(List<TrainingHouseRoom> trainingHouseRooms)
    {
        if(_trainingHouseRoomViews.Count < trainingHouseRooms.Count)
            for (int i = _trainingHouseRoomViews.Count - 1; i < trainingHouseRooms.Count; i++)
                _trainingHouseRoomViews.Add(Instantiate(roomPrefab, parent));

        for (int i = 0; i < _trainingHouseRoomViews.Count; i++)
        {
            _trainingHouseRoomViews[i].gameObject.SetActive(i < trainingHouseRooms.Count);
            if (i < trainingHouseRooms.Count) _trainingHouseRoomViews[i].UpdateView(trainingHouseRooms[i]);
        }
    }
}
