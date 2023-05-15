using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class RestoreDefaultSliderValue : MonoBehaviour
{
    [Serializable]
    public struct DefaultSliderValue
    {
        public PinchSlider slider;
        public float value;
    }
    public DefaultSliderValue[] defaultValues;

    public void RestoreDefaults()
    {
        for (int i = 0; i < defaultValues.Length; i++)
        {
            defaultValues[i].slider.SliderValue = defaultValues[i].value;
        }
    }
}
