using SandBox.DataInformation;
using SandBox.Tree.InfomartionPopup;
using TMPro;
using UIFlow;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InfoPopup : MonoBehaviour
{
    // test

    [SerializeField] private UIObject popup;

    [SerializeField] [Space] private TMP_Text title;

    [SerializeField] [Space] private Transform normalParent;
    [SerializeField] private TMP_Text normalContent;

    [SerializeField] [Space] private Transform scrollParent;
    [SerializeField] private TMP_Text scrollContent;


    private RectTransform _scrollContentRect;

    private RectTransform ScrollContentRect
    {
        get
        {
            _scrollContentRect ??= scrollContent.GetComponent<RectTransform>();
            return _scrollContentRect;
        }
    }

    [SerializeField] [Space] private float scrollPoint;
    [SerializeField] private ContentSizeFitter sizeFitter;

    private void OnEnable()
    {
        switch (popup.Data)
        {
            case InfoPopupData2 data:
                UpdateView(data);
                break;
            case InfoPopupPreset preset:
                UpdateView(preset.data);
                break;
        }
    }

    public void UpdateView(InfoPopupData2 data)
    {
        scrollParent.gameObject.SetActive(true);
        title.SetText(data.title);
        title.fontSize = data.titleFontSize;

        scrollContent.SetText(data.content);
        sizeFitter.SetLayoutVertical();

        var showAsNormal = ScrollContentRect.sizeDelta.y < scrollPoint;

        normalParent.gameObject.SetActive(showAsNormal);
        scrollParent.gameObject.SetActive(!showAsNormal);

        if (showAsNormal)
        {
            normalContent.SetText(data.content);
        }

        // normalContent.alignment = TextAlignmentOptions.
    }

    private void OnValidate()
    {
        Assert.IsNotNull(popup);
        Assert.IsNotNull(title);
        Assert.IsNotNull(normalParent);
        Assert.IsNotNull(normalContent);
        Assert.IsNotNull(scrollParent);
        Assert.IsNotNull(scrollContent);
        Assert.IsNotNull(sizeFitter);
    }
}