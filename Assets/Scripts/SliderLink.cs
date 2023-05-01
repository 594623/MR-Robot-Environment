using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SliderLink : MonoBehaviour
{
    public PinchSlider slider1;
    public PinchSlider slider2;

    private bool isUpdating = false;

    void Start()
    {
        slider1.OnValueUpdated.AddListener(OnSlider1Updated);
    }

    private void OnSlider1Updated(SliderEventData eventData)
    {
        if (!isUpdating)
        {
            isUpdating = true;
            slider2.SliderValue = slider1.SliderValue;
            isUpdating = false;
        }
    }
}
