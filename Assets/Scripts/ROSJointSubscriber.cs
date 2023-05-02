using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class ROSJointSubscriber : MonoBehaviour
{
    public string topicName;
    public GameObject baseObject;
    [Space]
    [Header("Debug Utilities")]
    public bool manualJointValues = false;
    public float[] jointValues = { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

    private List<float> jointRads;

    // Start is called before the first frame update
    void Start()
    {
        if (!manualJointValues)
        {
            ROSConnection.GetOrCreateInstance().Subscribe<JointStateMsg>(topicName, JointUpdate);
        } else if (jointValues.Length != 6)
        {
            Debug.LogError("There must be exactly 6 manual joint values entered");
        }
    }

    void Update()
    {
        // Moves to manually chosen values if enabled
        if (manualJointValues && jointValues.Length == 6)
        {
            jointRads = new List<float>();
            for (int i = 0; i < jointValues.Length; i++)
            {
                jointRads.Add((float)(Math.PI / 180) * jointValues[i]);
            }
            baseObject.GetComponent<ArticulationBody>().SetDriveTargets(jointRads);
        }
    }

    void JointUpdate(JointStateMsg jointData)
    {
        List<float> positions = new List<float>();
        for (int i = 0; i < jointData.position.Length; i++)
        {
            positions.Add((float) jointData.position[i]);
        }

        baseObject.GetComponent<ArticulationBody>().SetDriveTargets(positions);
    }
}
