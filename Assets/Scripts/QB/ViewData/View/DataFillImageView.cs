using UnityEngine;
using UnityEngine.UI;

namespace QB.ViewData
{
    [DefaultExecutionOrder(-200)]
    [RequireComponent(typeof(Image))]
    public class DataFillImageView : MonoBehaviour, IDataMemberView
    {
        [SerializeField] [Space] private DataCoreView coreView;
        [SerializeField] private string valueMemberName;
        [SerializeField] private string maxValueMemberName;

        [SerializeField] [Space] private float fillDefault;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();

            coreView.AddCallbackUpdate(this);
        }

        public void UpdateDefault()
        {
            _image.fillAmount = fillDefault;
        }

        public void UpdateView(object data)
        {
            var fillValue = System.Convert.ToSingle(data.Get(valueMemberName)) / System.Convert.ToSingle(data.Get(maxValueMemberName));
            _image.fillAmount = fillValue;
        }
    }
}