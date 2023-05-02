using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private string[] ignoredObjects = { "base_link_inertia", "shoulder_link", "upper_arm_link", "forearm_link", "wrist_1_link", "wrist_2_link", "wrist_3_link" };

    private void OnCollisionEnter(Collision collision)
    {
        // Ignores certain Game Objects such as other joints of the Articulation Body
        if (ignoredObjects.Contains(collision.gameObject.name)) return;
        // Logs the collision to the console
        Debug.Log("Collision detected between " + gameObject.name + " and " + collision.gameObject.name);
    }
}
