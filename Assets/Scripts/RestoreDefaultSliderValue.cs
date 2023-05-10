using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class SliderRestoreDefautl : MonoBehaviour
{
    [Serializable]
    public struct DefaultSliderValue
    {
        public PinchSlider slider;
        public float value;
    }
    public DefaultSliderValue[] defaultValues;
    public Dictionary<PinchSlider, float> myDictionary;
    public PinchSlider tableHeight;
    public float tableHeightDefaultValue;
    public PinchSlider robotDistance;
    public float robotDistanceDefaultValue;
    public PinchSlider robotHeight;
    public float robotHeightDefaultValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestoreDefaults()
    {
        //tableHeight.SliderValue = tableHeightDefault;
    }
}
