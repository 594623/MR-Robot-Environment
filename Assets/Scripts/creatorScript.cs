using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Diagnostics;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit.Input;


public class creatorScript : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab to be instantiated
    public GameObject capsulePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;
    public GameObject linePrefab;
    private GameObject lineObject;
    private LineRenderer lineRenderer;


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

        
    }

    private void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        Destroy(gameObject);
    }

    // Function for spawning a cube
    public void SpawnCube()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newCube.AddComponent<Interactable>();
        interactable.OnClick.AddListener(() =>
        {
            if (deleteMode)
            {
                Destroy(newCube);
                prefabList.Remove(newCube);
            }
        });

        CheckGrav(newCube);
        prefabList.Add(newCube);
        /* WIP for a placing mode
         *  TogglePlaceMode();
        if (placeMode)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo))
            {
                GameObject newCube = Instantiate(cubePrefab, hitInfo.point, Quaternion.identity);

                // Add an Interactable component and set its OnClick event to OnObjectClicked
                Interactable interactable = newCube.AddComponent<Interactable>();
              //  interactable.OnClick.AddListener(OnObjectClicked);
                CheckGrav(newCube);
                prefabList.Add(newCube);

                // Update line renderer end position to the new cube's position
                lineRenderer.SetPosition(1, newCube.transform.position);
            }
        
    }
        */
    }
    // Function for spawning a sphere
    public void SpawnSphere()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newSphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newSphere.AddComponent<Interactable>();
        interactable.OnClick.AddListener(() =>
        {
            if (deleteMode)
            {
                Destroy(newSphere);
                prefabList.Remove(newSphere);
            }
        });

        CheckGrav(newSphere);
        prefabList.Add(newSphere);
    }
    // Function for spawning a capsule
    public void SpawnCapsule()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCapsule = Instantiate(capsulePrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newCapsule.AddComponent<Interactable>();
        interactable.OnClick.AddListener(() =>
        {
            if (deleteMode)
            {
                Destroy(newCapsule);
                prefabList.Remove(newCapsule);
            }
        });

        CheckGrav(newCapsule);
        prefabList.Add(newCapsule);
    }
    // Function for spawning a cylinder
    public void SpawnCylinder()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        GameObject newCylinder = Instantiate(cylinderPrefab, spawnPosition, Quaternion.identity);

        // Add an Interactable component and set its OnClick event to OnObjectClicked
        // Add an Interactable component and set its OnClick event to OnObjectClicked
        Interactable interactable = newCylinder.AddComponent<Interactable>();
        interactable.OnClick.AddListener(() =>
        {
            if (deleteMode)
            {
                Destroy(newCylinder);
                prefabList.Remove(newCylinder);
            }
        });

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
                rb.isKinematic = !rb.isKinematic;
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
                bool isKinematic = lastObjectRigidbody.isKinematic;

                // Set the useGravity setting of the new object
                Rigidbody newObjectRigidbody = newObject.GetComponent<Rigidbody>();

                if (newObjectRigidbody != null)
                {
                    newObjectRigidbody.isKinematic = isKinematic;
                    newObjectRigidbody.useGravity = useGravity;
                }
            }
        }
    }
    // Simply toggles DeleteMode on or off
    public void ToggleDeleteMode()
    {
        deleteMode = !deleteMode;
    }

    // Simply toggles PlaceMode on or off
    public void TogglePlaceMode()
    {
        placeMode = !placeMode;
        if (placeMode)
        {
            lineObject = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
            lineRenderer = lineObject.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.useWorldSpace = true;
            lineRenderer.SetPosition(0, Camera.main.transform.position);
            lineRenderer.SetPosition(1, Camera.main.transform.position + Camera.main.transform.forward * 2f);
        }
        else
        {
            Destroy(lineObject);
        }
    }

}
