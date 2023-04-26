using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatorScript : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab to be instantiated
    public GameObject capsulePrefab; 
    public GameObject spherePrefab; 
    private List<GameObject> prefabList = new List<GameObject> ();
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
        prefabList.Add(newCube);
    }

    public void SpawnSphere()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newSphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
        prefabList.Add(newSphere);
    }

    public void SpawnCapsule()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCapsule = Instantiate(capsulePrefab, spawnPosition, Quaternion.identity);
        prefabList.Add(newCapsule);
    }

    public void deleteLast()
    {
        if (prefabList.Count > 0)
        {
            GameObject lastPrefab = prefabList[prefabList.Count - 1];
            prefabList.RemoveAt(prefabList.Count - 1);
            Destroy(lastPrefab);
        }
    }

    public void deleteAllShapes()
    {
        foreach (GameObject prefab in prefabList)
        {
            Destroy(prefab);
        }
        prefabList.Clear();
    }
}
