using System.Collections.Generic;
using System.Linq;
using GEvent;
using QB.Collection;
using TigerForge;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerInfo
{
    public class SelectRoomPanel : MonoBehaviour
    {
        [SerializeField] private List<int> ticketBasedRooms;
        [SerializeField] private int roomDefault;
        [SerializeField] private DefaultableDictionary<int, string> roomNames;

        [SerializeField] private TMP_Text roomNameText;
        [SerializeField] private Button nextRoomButton;
        [SerializeField] private Button backRoomButton;

        private int _currentRoom;

        public int GetCurrentRoom()
        {
            return 0;
        }

        private void Awake()
        {
            SelectRoom(roomDefault);

            nextRoomButton.onClick.AddListener(SelectNextRoom);
            backRoomButton.onClick.AddListener(SelectBackRoom);
        }

        private void SelectRoom(int ticketRoom)
        {
            EventManager.EmitEventData(EventName.Client.Battle.PvpRoom, data: ticketRoom);

            _currentRoom = ticketRoom;
            roomNameText.SetText(roomNames[_currentRoom]);
        }

        private void SelectNextRoom()
        {
            var currentRoomIndex = ticketBasedRooms.FindIndex(room => room == _currentRoom);
            var nextRoomIndex = currentRoomIndex + 1;

            var isValidRoom = nextRoomIndex < ticketBasedRooms.Count;
            var roomToSelect = isValidRoom ? ticketBasedRooms[nextRoomIndex] : ticketBasedRooms.First();

            SelectRoom(roomToSelect);
        }

        private void SelectBackRoom()
        {
            var currentRoomIndex = ticketBasedRooms.FindIndex(room => room == _currentRoom);
            var backRoomIndex = currentRoomIndex - 1;

            var isValidRoom = currentRoomIndex > (int)default;
            var roomToSelect = isValidRoom ? ticketBasedRooms[backRoomIndex] : ticketBasedRooms.Last();

            SelectRoom(roomToSelect);
        }
    }
}