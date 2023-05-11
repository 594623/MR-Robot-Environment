using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SliderLink : MonoBehaviour
{
    public PinchSlider[] sliders;

    private bool isLinked = false;
    private bool isUpdating = false;

    void Start()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].OnValueUpdated.AddListener(OnSliderUpdated);
        }
    }

    public void ToggleSliderLink()
    {
        isLinked = !isLinked;
    }

    private void OnSliderUpdated(SliderEventData eventData)
    {
        if (isLinked && !isUpdating)
        {
            isUpdating = true;
            for (int i = 0; i < sliders.Length; i++)
            {
                sliders[i].SliderValue = eventData.NewValue;
            }
            isUpdating = false;
        }
    }
}
