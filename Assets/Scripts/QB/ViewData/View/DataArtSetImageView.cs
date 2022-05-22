using QB.ViewData;
using UnityEngine;
using UnityEngine.UI;

namespace ViewData
{
    [DefaultExecutionOrder(-200)]
    [RequireComponent(typeof(Image))]
    public class DataArtSetImageView : MonoBehaviour, IDataMemberView
    {
        [SerializeField] [Space] private DataCoreView coreView;
        [SerializeField] private ScriptableObject artSet;
        [SerializeField] private string memberName;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            coreView.AddCallbackUpdate(this);
        }

        public void UpdateDefault()
        {
            _image.sprite = ((IArtSet) artSet)?.GetSprite(new object());
        }

        public void UpdateView(object data)
        {
            _image.sprite = ((IArtSet) artSet)?.GetSprite(data.Get(memberName));
        }

        private void OnValidate()
        {
            if (!(artSet is IArtSet)) artSet = null;
        }
    }
}