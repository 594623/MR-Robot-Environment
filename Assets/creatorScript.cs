using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatorScript : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab to be instantiated
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is clicked
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f; // Calculate the spawn position in front of the camera
            GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity); // Create a new instance of the cube prefab at the spawn position
        }
    }
}
