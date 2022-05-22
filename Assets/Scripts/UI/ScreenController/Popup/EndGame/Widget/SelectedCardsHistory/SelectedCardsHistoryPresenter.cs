using System.Collections.Generic;
using GEvent;
using Network.Service.Implement;
using TigerForge;
using UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory.CardView;
using UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory.Utils;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace UI.ScreenController.Popup.EndGame.Widget.SelectedCardsHistory
{
    public class SelectedCardsHistoryPresenter : MonoBehaviour
    {
        [SerializeField, Space] private SelectedCardsHistoryPresenterMember selfPresenter;
        [SerializeField] private SelectedCardsHistoryPresenterMember opponentPresenter;

        [SerializeField, Space] private HistoryCardView selectedCardImagePrefab;
        [SerializeField] private int numberSlotInPage = 5;

        [SerializeField, Space] private Button nextPageButton;
        [SerializeField] private Button previousPageButton;

        private int _numberPageViewing;


        private void Awake()
        {
            EventManager.StartListening(EventName.Server.EndGame,
                () => Invoke(nameof(OnEndGame), EndGameServerService.DelayConfig.battleResultPopup));


            for (int i = 0; i < numberSlotInPage; i++)
            {
                selfPresenter.InstantiateCard(selectedCardImagePrefab);
                opponentPresenter.InstantiateCard(selectedCardImagePrefab);
            }

            const int viewPageStep = 1;
            nextPageButton.onClick.AddListener(() => ViewPage(_numberPageViewing + viewPageStep));
            previousPageButton.onClick.AddListener(() => ViewPage(_numberPageViewing - viewPageStep));
        }

        private void OnEnable() => OnEndGame();

        private void OnEndGame()
        {
            var response = EndGameServerService.Response;
            if(response.IsError) return;
            if (response.data.IsOpinionQuitPvp())
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            
            var endGameData = EndGameServerService.GetClientData();
            
            selfPresenter.selectCardHistoryList = SelectCardsHistoryUtils.ConvertToSet10Cards(endGameData.GetSelfInfo().historySelectedCards);
            opponentPresenter.selectCardHistoryList = SelectCardsHistoryUtils.ConvertToSet10Cards(endGameData.GetOpponentInfo().historySelectedCards);
            
            ViewFirstPage();
        }
        

        private void ViewFirstPage()
        {
            const int defaultPageView = 0;
            ViewPage(defaultPageView);
        }

        private void ViewPage(int pageNumber)
        {
            const int minPageCanView = 0;
            var maxPageCanView = Mathf.CeilToInt(f: (float)selfPresenter.selectCardHistoryList.Count / numberSlotInPage) - 1;

            nextPageButton.interactable = true;
            previousPageButton.interactable = true;


            if (pageNumber <= minPageCanView)
            {
                _numberPageViewing = minPageCanView;
                previousPageButton.interactable = false;
            }
            else if (pageNumber >= maxPageCanView)
            {
                _numberPageViewing = maxPageCanView;
                nextPageButton.interactable = false;
            }
            else
            {
                _numberPageViewing = pageNumber;
            }


            for (int i = 0; i < numberSlotInPage; i++)
            {
                var selectedCardHistoryIndex = i + numberSlotInPage * pageNumber;

                selfPresenter.ShowCard(index: i, selfPresenter.selectCardHistoryList[selectedCardHistoryIndex]);
                opponentPresenter.ShowCard(index: i, opponentPresenter.selectCardHistoryList[selectedCardHistoryIndex]);
            }
        }
    }


    [System.Serializable]
    public class SelectedCardsHistoryPresenterMember
    {
        [HideInInspector] public readonly List<HistoryCardView> SelectedCardImages = new List<HistoryCardView>();
        public List<SelectCardHistory> selectCardHistoryList;

        public Transform contentParent;

        public void InstantiateCard(HistoryCardView prefab)
        {
            SelectedCardImages.Add(Object.Instantiate(prefab, parent: contentParent));
        }

        public void SetActiveCard(int index, bool isActive)
        {
            SelectedCardImages[index].gameObject.SetActive(isActive);
        }

        public void ShowCard(int index, SelectCardHistory selectCardHistory)
        {
            SelectedCardImages[index].UpdateView(selectCardHistory);
            SetActiveCard(index, isActive: true);
        }
    }
}