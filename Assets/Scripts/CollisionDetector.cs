using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    [Space]
    [Header("Collisions with these objects will be ignored")]
    public GameObject[] ignoredObjects;
    [Header("Event(s) when a collision is detected")]
    public UnityEvent<Collision> OnCollision;

    private void OnCollisionEnter(Collision collision)
    {
        // Ignores certain Game Objects
        if (ignoredObjects.Contains(collision.gameObject)) return;
        // Logs the collision to the console
        Debug.Log("Collision detected between " + gameObject.name + " and " + collision.gameObject.name);
        // Invokes event listeners
        OnCollision?.Invoke(collision);
    }
}
