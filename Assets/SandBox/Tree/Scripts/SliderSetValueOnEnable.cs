using UnityEngine;
using UnityEngine.UI;

public class SliderSetValueOnEnable : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float value;
    
    private void OnEnable()=> slider.value = value;
}
