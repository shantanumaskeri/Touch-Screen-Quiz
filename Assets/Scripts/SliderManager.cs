using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{

    public AlphaManager alphaManager;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void CheckSliderScrollValue()
    {
        if (slider.value >= 1.0f)
        {
            alphaManager.InitializeFading();
        }
    }

}
