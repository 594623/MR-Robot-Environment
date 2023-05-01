using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Diagnostics;

public class creatorScript : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab to be instantiated
    public GameObject capsulePrefab; 
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;
    public float spawnOffset = 0.2f;
    public PinchSlider widthSlider;
    public PinchSlider heightSlider;
    public PinchSlider lengthSlider;

    private List<GameObject> prefabList = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
        /*
        // Add listeners to the pinch sliders' OnValueUpdated events
        widthSlider.OnValueUpdated.AddListener(OnWidthSliderUpdated);
        heightSlider.OnValueUpdated.AddListener(OnHeightSliderUpdated);
        lengthSlider.OnValueUpdated.AddListener(OnLengthSliderUpdated);
        */
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

    public void SpawnCylinder()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCylinder = Instantiate(cylinderPrefab, spawnPosition, Quaternion.identity);
        prefabList.Add(newCylinder);
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
    /*
    private void OnWidthSliderUpdated(SliderEventData eventData)
    {
        float widthScale = eventData.NewValue;
        ScalePrefabListObjects(widthScale, 1f, 1f);
        UnityEngine.Debug.Log(widthScale);
    }

    private void OnHeightSliderUpdated(SliderEventData eventData)
    {
        float heightScale = eventData.NewValue;
        ScalePrefabListObjects(1f, heightScale, 1f);
        UnityEngine.Debug.Log(heightScale);
    }

    private void OnLengthSliderUpdated(SliderEventData eventData)
    {
        float lengthScale = eventData.NewValue;
        ScalePrefabListObjects(1f, 1f, lengthScale);
    }

    private void ScalePrefabListObjects(float widthScale, float heightScale, float lengthScale)
    {
        foreach (GameObject obj in prefabList)
        {
            Vector3 originalScale = obj.transform.localScale;
            Vector3 newScale = new Vector3(originalScale.x * widthScale, originalScale.y * heightScale, originalScale.z * lengthScale);
            obj.transform.localScale = newScale;
        }
    }
    */
}
