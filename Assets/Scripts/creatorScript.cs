using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Diagnostics;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.XR;



public class creatorScript : MonoBehaviour
{
    public GameObject cubePrefab; // Reference to the cube prefab to be instantiated
    public GameObject capsulePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;
    public GameObject linePrefab;
    private GameObject lineObject;
    public GameObject selectedPrefab;
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
    private int objectsSpawned = 0;
    public float spawnDelay = 1.5f; // The delay between each object spawn
    private float lastSpawnTime = 0f; // The time of the last object spawn

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
            if (placeMode)
            {
                InputDevice rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

                // Check for air tap on controller
                bool airTap = false;
                if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
                {
                    airTap = true;
                }
                else if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonPressed) && primaryButtonPressed)
                {
                    airTap = true;
                }

            if (airTap && Time.time - lastSpawnTime > spawnDelay)
            {
                lastSpawnTime = Time.time;
                SpawnObject(selectedPrefab);
                objectsSpawned++;
                return;
            }

            // Update line renderer
            Vector3 handPosition;
                if (rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out handPosition))
                {
                    Vector3 lineEndPosition = handPosition + Camera.main.transform.forward * 0.5f;
                    lineRenderer.SetPosition(0, handPosition);
                    lineRenderer.SetPosition(1, lineEndPosition);
                }
            }
        }


        private void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        Destroy(gameObject);
    }

    // Function for spawning a cube
    public void SpawnCube()
    {
        TogglePlaceMode();
        selectedPrefab = cubePrefab;
    }
    // Function for spawning a sphere
    public void SpawnSphere()
    {
        TogglePlaceMode();
        selectedPrefab = spherePrefab;
    }
    // Function for spawning a capsule
    public void SpawnCapsule()
    {
        TogglePlaceMode();
        selectedPrefab = capsulePrefab;
    }
    // Function for spawning a cylinder
    public void SpawnCylinder()
    {
        TogglePlaceMode();
        selectedPrefab = cylinderPrefab;
    }

    public void SpawnObject(GameObject prefab)
    {
       
                Vector3 spawnPosition = lineRenderer.GetPosition(1);
                GameObject newObj = Instantiate(prefab, spawnPosition, Quaternion.identity);

                // Add an Interactable component and set its OnClick event to OnObjectClicked
                Interactable interactable = newObj.AddComponent<Interactable>();
                interactable.OnClick.AddListener(() =>
                {
                    if (deleteMode)
                    {
                        Destroy(newObj);
                        prefabList.Remove(newObj);
                    }
                });

                CheckGrav(newObj);
                prefabList.Add(newObj);
            
     
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
