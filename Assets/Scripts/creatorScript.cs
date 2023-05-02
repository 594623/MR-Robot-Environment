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

    public PinchSlider widthSlider;
    public PinchSlider heightSlider;
    public PinchSlider lengthSlider;

    public bool deleteMode = false;
    public bool placeMode = false;

    public float spawnOffset = 0.2f;
    private float widthScale = 1f;
    private float heightScale = 1f;
    private float lengthScale = 1f;

    private List<GameObject> prefabList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {

        // Add listeners to the pinch sliders' OnValueUpdated events
        widthSlider.OnValueUpdated.AddListener(OnWidthSliderUpdated);
        heightSlider.OnValueUpdated.AddListener(OnHeightSliderUpdated);
        lengthSlider.OnValueUpdated.AddListener(OnLengthSliderUpdated);

    }

    // Update is called once per frame
    // Commented out code for deleting objects in unity rather than the hololens
    void Update()
    {
        /* 
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the clicked object is in the prefabList and delete it if it is
                if (prefabList.Contains(hit.collider.gameObject))
                {
                    prefabList.Remove(hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
        */
    }
    // Function for spawning a cube
    public void SpawnCube()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newCube.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectClicked);
        CheckGrav(newCube);
        prefabList.Add(newCube);
    }
    // Function for spawning a sphere
    public void SpawnSphere()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newSphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newSphere.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectClicked);

        CheckGrav(newSphere);
        prefabList.Add(newSphere);
    }
    // Function for spawning a capsule
    public void SpawnCapsule()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCapsule = Instantiate(capsulePrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newCapsule.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectClicked);

        CheckGrav(newCapsule);
        prefabList.Add(newCapsule);
    }
    // Function for spawning a cylinder
    public void SpawnCylinder()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCylinder = Instantiate(cylinderPrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newCylinder.AddComponent<Interactable>();
        interactable.OnClick.AddListener(OnObjectClicked);

        CheckGrav(newCylinder);
        prefabList.Add(newCylinder);
    }
    // Function for deleting the last spawned object
    public void deleteLast()
    {
        if (prefabList.Count > 0)
        {
            GameObject lastPrefab = prefabList[prefabList.Count - 1];
            prefabList.RemoveAt(prefabList.Count - 1);
            Destroy(lastPrefab);
        }
    }
    // Function for deleting all the spawned objects
    public void deleteAllShapes()
    {
        foreach (GameObject prefab in prefabList)
        {
            Destroy(prefab);
        }
        prefabList.Clear();
    }

    public void ToggleGravity()
    {
        foreach (GameObject obj in prefabList)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = !rb.useGravity;
            }
        }
    }

    // Functions for updating the size of the placed objects one for x,y and z respectively
    private void OnWidthSliderUpdated(SliderEventData eventData)
    {
        widthScale = eventData.NewValue;
        ScalePrefabListObjects();
    }

    private void OnHeightSliderUpdated(SliderEventData eventData)
    {
        heightScale = eventData.NewValue;
        ScalePrefabListObjects();
    }

    private void OnLengthSliderUpdated(SliderEventData eventData)
    {
        lengthScale = eventData.NewValue;
        ScalePrefabListObjects();
    }
    // The function that performs the actual scaling on all the spawned objects
    private void ScalePrefabListObjects()
    {
        foreach (GameObject obj in prefabList)
        {
            Vector3 originalScale = obj.transform.localScale;
            Vector3 newScale = new Vector3(widthScale, heightScale, lengthScale);
            obj.transform.localScale = newScale;
        }
    }

    //Function for checking if new objects should have gravity disabled or enabled based on the last placed object
    public void CheckGrav(GameObject newObject)
    {
        if (prefabList.Count > 0)
        {
            // Get the last spawned object
            GameObject lastObject = prefabList[prefabList.Count - 1];

            // Check if it has a rigidbody component
            Rigidbody lastObjectRigidbody = lastObject.GetComponent<Rigidbody>();

            if (lastObjectRigidbody != null)
            {
                // Get the useGravity setting of the last spawned object
                bool useGravity = lastObjectRigidbody.useGravity;

                // Set the useGravity setting of the new object
                Rigidbody newObjectRigidbody = newObject.GetComponent<Rigidbody>();

                if (newObjectRigidbody != null)
                {
                    newObjectRigidbody.useGravity = useGravity;
                }
            }
        }
    }
    // If deletemode is enabled the clicked object should be deleted.
    public void OnObjectClicked()
    {
        if (deleteMode)
        {
            // Get the clicked object
            GameObject clickedObject = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

            // Remove it from the prefabList and destroy it
            if (prefabList.Contains(clickedObject))
            {
                prefabList.Remove(clickedObject);
                Destroy(clickedObject);
            }
        }
    }
    // Simply toggles DeleteMode on or off
    public void ToggleDeleteMode()
    {
        deleteMode = !deleteMode;
    }

}
