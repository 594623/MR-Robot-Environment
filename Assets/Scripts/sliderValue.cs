using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using System.Security.Cryptography;
using TMPro;

public class SliderValue : MonoBehaviour
{
    public float multiplier = 1;
    public int decimals = 2;
    public string suffix;

    private PinchSlider sliderObj;
    private TextMeshPro textObj;
    // Update is called once per frame
    void Update()
    {
    }

    void Start()
    {
        textObj = gameObject.GetComponent<TextMeshPro>();
        sliderObj = gameObject.transform.parent.gameObject.GetComponent<PinchSlider>();
        textObj.text = GetSliderText(sliderObj.SliderValue);
        sliderObj.OnValueUpdated.AddListener(OnPinchSliderValueChanged);
    }

    string GetSliderText(float sliderValue)
    {
        decimal value = new decimal(sliderValue * multiplier);
        string text = decimal.Round(value, decimals).ToString();
        if (suffix.Length > 0)
        {
            text += " " + suffix;
        }
        return text;
    }

    void OnPinchSliderValueChanged(SliderEventData eventData)
    {
        textObj.text = GetSliderText(sliderObj.SliderValue);
        Debug.Log(textObj.text);
    }
}