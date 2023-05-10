using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Diagnostics;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine.XR;
using System.Globalization;
using TMPro;

public class creatorScript : MonoBehaviour
{
    // References to the various prefabs that are being used
    public GameObject cubePrefab;
    public GameObject capsulePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;
   // public GameObject linePrefab;
 //   private GameObject lineObject;
    private GameObject selectedPrefab;
  //  private LineRenderer lineRenderer;

    // References to the sliders for scaling the objects
    public PinchSlider widthSlider;
    public PinchSlider heightSlider;
    public PinchSlider lengthSlider;

    // booleans for placeing and deletemode
    public bool deleteMode = false;
    public bool placeMode = false;
    //Reference to the text objects which says if we are in delete mode or not
    public TextMeshPro TextMeshProObject;

    // Some predefined values
    public float spawnOffset = 0.2f;

    public float widthScale = 0.2f;
    public float heightScale = 0.2f;
    public float lengthScale = 0.2f;
  //  private int objectsSpawned = 0;
  //  public float spawnDelay = 1.5f; // The delay between each object spawn
  //  private float lastSpawnTime = 0f; // The time of the last object spawn

    //List with all the created gameobjects
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
    // First checks if we are in placemode or not, then some logic for checking for an airtap. If airtap is detected and the spawn delay time
    //  is less than the time between the last spawned object it creates another object with the help of SpawnOjbect() with a reference to the selectedPrefab
    void Update()
    {
        /*
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
            */
    }


    // Toggles placemode and selects cube as current prefab
    public void SpawnCube()
    {
        //TogglePlaceMode();
        selectedPrefab = cubePrefab;
            SpawnObject(selectedPrefab);
    }
    // Toggles placemode and selects sphere as current prefab
    public void SpawnSphere()
    {
        //TogglePlaceMode();
        selectedPrefab = spherePrefab;
            SpawnObject(selectedPrefab);
        }
    // Toggles placemode and selects capsule as current prefab
    public void SpawnCapsule()
    {
        //TogglePlaceMode();
        selectedPrefab = capsulePrefab;
            SpawnObject(selectedPrefab);
        }
    // Toggles placemode and selects cylinder as current prefab
    public void SpawnCylinder()
    {
       // TogglePlaceMode();
        selectedPrefab = cylinderPrefab;
            SpawnObject(selectedPrefab);
        }
    // Function for spawning the selected prefab 
    public void SpawnObject(GameObject prefab)
    {
        prefab.transform.localScale = new Vector3(widthScale, heightScale, lengthScale);
        // Sets spawnpostion at end of linerenderer
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        // Should add scaling based on current slider values here.
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
        // Checks if gravity should be toggeled on or off
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
    // Function for toggeling gravity for spawned objects also toggles Kinematic
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
        UnityEngine.Debug.Log(eventData.NewValue);
        widthScale = eventData.NewValue;

        //ScalePrefabListObjects();
    }

    private void OnHeightSliderUpdated(SliderEventData eventData)
    {
        heightScale = eventData.NewValue;

        //ScalePrefabListObjects();
    }

    private void OnLengthSliderUpdated(SliderEventData eventData)
    {
        lengthScale = eventData.NewValue;

        //ScalePrefabListObjects();
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
        // Show the text element and set its text
        if (deleteMode)
        {
            TextMeshProObject.gameObject.SetActive(true);
            TextMeshProObject.text = "DeleteMode active";
        }
        else
        {
            TextMeshProObject.gameObject.SetActive(false);
        }
    }
        /*
    // Simply toggles PlaceMode on or off and some lineobject logic
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
        */
}
