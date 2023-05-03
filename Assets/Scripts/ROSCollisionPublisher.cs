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
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<BoolMsg>(topicName);
    }

    void Update()
    {
        
    }

    public void onCollision(Collision collision)
    {
        OnStopSignal?.Invoke();

        ros.Publish(topicName, new BoolMsg(true));

        OnContinueSignal?.Invoke();
    }
}
