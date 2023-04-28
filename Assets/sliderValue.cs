using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Security.Cryptography;
using TMPro;
using System.Diagnostics;

public class sliderValue : MonoBehaviour
{
    public PinchSlider PinchSliderObject;
    public TextMeshPro TextMeshProObject;
    // Update is called once per frame
    void Update()
    {
    }

    void Start()
    {
        // Get a reference to the PinchSlider component
        TextMeshProObject.text = PinchSliderObject.SliderValue.ToString("F1");
        PinchSliderObject.OnValueUpdated.AddListener(OnPinchSliderValueChanged);
    }

    void OnPinchSliderValueChanged(SliderEventData eventData)
    {
        TextMeshProObject.text = PinchSliderObject.SliderValue.ToString("F1");
    }
}