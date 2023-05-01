using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
//using JointState = RosMessageTypes.JointState;

public class ROSJointSubscriber : MonoBehaviour
{
    GameObject baseObject;

    // Start is called before the first frame update
    void Start()
    {
        baseObject = transform.GetChild(1).GetChild(3).gameObject;
        // TODO: Subscribe to the "joints" ROS topic through the ROS Connection
        //ROSConnection.GetOrCreateInstance().Subscribe<JointState>("joints", JointUpdate);
    }

    void JointUpdate(/*JointState jointData*/)
    {
        // TODO: Update the robot model's joint data with the real robot's joint data
        List<float> positions = new List<float>();
        //positions.AddRange(jointData.position);
        //baseObject.getComponent<ArticulationBody>().setDriveTargets(positions);
    }
}
