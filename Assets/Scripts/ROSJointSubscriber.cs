using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROSJointSubscriber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // TODO: Subscribe to the "joints" ROS topic through the ROS Connection
        //ROSConnection.GetOrCreateInstance().Subscribe<JointState>("joints", JointUpdate);
    }

    void JointUpdate(/*JointState jointData*/)
    {
        // TODO: Update the robot model's joint data with the real robot's joint data
    }
}
