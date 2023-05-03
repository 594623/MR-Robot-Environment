using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Std;

public class ROSCollisionPublisher : MonoBehaviour
{
    public string topicName;
    // Events that may be useful
    public UnityEvent OnStopSignal;
    public UnityEvent OnContinueSignal;

    private ROSConnection ros;

    void Start()
    {
        // TODO: Decide what message to send to ROS
        ros = ROSConnection.GetOrCreateInstance();
        //ros.RegisterPublisher<MessageType>(topicName);
    }

    void Update()
    {
        
    }

    public void onCollision(Collision collision)
    {
        OnStopSignal?.Invoke();
        // Was ros.Publish(topicName, data);
        // ros.Publish(topicName, new Bool(true));

        OnContinueSignal?.Invoke();
    }
}
