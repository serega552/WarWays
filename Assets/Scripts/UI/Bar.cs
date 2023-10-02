using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Slider Slider;

    public void OnValueChanged(float value, float maxvalue)
    {
        Slider.value = value / maxvalue;
    }
}