using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class xAdjuster : MonoBehaviour
{
    public float movementRange = 1.0f;
    public Transform targetObject;

    private void Start()
    {
        // Add a listener to the pinch slider's OnValueUpdated event
        GetComponent<PinchSlider>().OnValueUpdated.AddListener(OnPinchSliderUpdated);
    }

    private void OnPinchSliderUpdated(SliderEventData eventData)
    {
        // Get the current value of the pinch slider (between 0 and 1)
        float sliderValue = eventData.NewValue;

        // Move the target object along the y-axis based on the slider value
        Vector3 newPosition = (targetObject.position);
        newPosition.x = (sliderValue - 0.5f) * movementRange * 2.0f;
        targetObject.position = newPosition;

    }
}
