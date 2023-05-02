using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public string[] ignoredObjects;
    public UnityEvent OnCollision;

    private void OnCollisionEnter(Collision collision)
    {
        // Ignores certain Game Objects
        if (ignoredObjects.Contains(collision.gameObject.name)) return;
        // Logs the collision to the console
        Debug.Log("Collision detected between " + gameObject.name + " and " + collision.gameObject.name);
        OnCollision?.Invoke();
        //
    }
}
