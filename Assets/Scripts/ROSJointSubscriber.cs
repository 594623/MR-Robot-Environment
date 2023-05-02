using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Sensor;

public class ROSJointSubscriber : MonoBehaviour
{
    GameObject baseObject;

    // Start is called before the first frame update
    void Start()
    {
        baseObject = transform.GetChild(1).GetChild(3).gameObject;
        ROSConnection.GetOrCreateInstance().Subscribe<JointStateMsg>("joints", JointUpdate);
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
