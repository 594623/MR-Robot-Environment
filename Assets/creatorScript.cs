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
        
    }

    public void SpawnCube()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }

}
