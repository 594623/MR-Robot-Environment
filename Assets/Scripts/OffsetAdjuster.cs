using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.UI;

public class OffsetAdjuster : MonoBehaviour
{
    public OffsetMode offsetMode;
    public GameObject targetObject;
    public GameObject resizedObject;
    public GameObject robotObject;
    public float movementRange = 1.0f;

    private bool isRobotMoving = false;
    private RobotTeleporter teleporter;

    public enum OffsetMode
    {
        TableHeight,
        RobotDistance,
        RobotHeight
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PinchSlider>().OnValueUpdated.AddListener(OnPinchSliderUpdated);
        if (robotObject != null)
        {
            teleporter = robotObject.GetComponent<RobotTeleporter>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPinchSliderUpdated(SliderEventData eventData)
    {
        // Get the current value of the pinch slider (between 0 and 1)
        float sliderValue = eventData.NewValue;

        Vector3 targetPos = targetObject.transform.localPosition;
        Vector3 resizedPos = resizedObject.transform.localPosition;
        Vector3 resizedScale = resizedObject.transform.localScale;

        // Offsets floor 0 - 1 meters
        if (offsetMode == OffsetMode.TableHeight)
        {
            float floorHeight = 0.5f;
            float tableTopHeight = 0.025f;
            // The floor is x meters below the top of the table. 
            
            targetPos.y = -sliderValue - floorHeight / 2;
            // The table body is resized
            resizedPos.y = -sliderValue / 2 - tableTopHeight / 2;
            resizedScale.y = sliderValue - tableTopHeight;
            if (resizedScale.y < 0)
            {
                resizedScale.y = 0;
            }
        }
        else if (offsetMode == OffsetMode.RobotDistance)
        {
            isRobotMoving = true;
            resizedPos.z = sliderValue;
        }
        // Offsets robot height 0 - 10 cm
        else if (offsetMode == OffsetMode.RobotHeight)
        {
            isRobotMoving = true;
            targetPos.y = sliderValue / 10;
            resizedPos.y = sliderValue / 20;
            resizedScale.y = sliderValue / 10;

            resizedObject.SetActive(resizedScale.y != 0);
        }

        targetObject.transform.localPosition = targetPos;
        if (isRobotMoving)
        {
            teleporter.TeleportRobot(targetObject);
        }
        resizedObject.transform.localPosition = resizedPos;
        resizedObject.transform.localScale = resizedScale;

        // Move the target object along the y-axis based on the slider value
        //Vector3 newPosition = targetObject.position;
        //newPosition.y = (sliderValue - 0.5f) * movementRange * 2.0f;
        //targetObject.position = newPosition;

        isRobotMoving = false;
    }
}
