using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Robotics.ROSTCPConnector;
using RosMessageTypes.Geometry;

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
        ros.RegisterPublisher<Vector3Msg>(topicName);
    }

    void Update()
    {
        
    }

    public void OnCollision(Collision collision)
    {
        OnStopSignal?.Invoke();

        Vector3 normal = collision.GetContact(0).normal;

        Debug.Log(normal);

        ros.Publish(topicName, new Vector3Msg(normal.x, normal.y, normal.z));
    }

    public void OnSeparation(Collision collision)
    {
        ros.Publish(topicName, new Vector3Msg());

        OnContinueSignal?.Invoke();
    }
}
