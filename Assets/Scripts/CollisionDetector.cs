using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("base_link_inertia")) return;

        Debug.Log("Collision detected between " + gameObject.name + " and " + collision.gameObject.name);
    }
}
