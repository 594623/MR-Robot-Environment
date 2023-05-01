using System.Collections;
using System.Collections.Generic;
using Unity.Robotics;
using Unity.Robotics.UrdfImporter.Control;
using UnityEngine;

/// <summary>
/// Edited version of URDFImporter's Controller class.
/// </summary>
public class CustomRobotController : Controller
{
        private ArticulationBody[] articulationChain;
        // Stores original colors of the part being highlighted
        private Color[] prevColor;
        private int previousIndex;
        private IEnumerator coroutine;
        private bool coroutineIsRunning = false;
        private bool movingPositive = false;
        private bool movingNegative = false;
        // TEST
        // private int counter = 0;

    void Start()
    {
        previousIndex = selectedIndex = 2;
        this.gameObject.AddComponent<FKRobot>();
        articulationChain = this.GetComponentsInChildren<ArticulationBody>();
        int defDyanmicVal = 10;
        foreach (ArticulationBody joint in articulationChain)
        {
            joint.gameObject.AddComponent<JointControl>();
            joint.jointFriction = defDyanmicVal;
            joint.angularDamping = defDyanmicVal;
            ArticulationDrive currentDrive = joint.xDrive;
            currentDrive.forceLimit = forceLimit;
            joint.xDrive = currentDrive;
        }
        DisplaySelectedJoint(selectedIndex);
        StoreJointColors(selectedIndex);
        
    }

    void SetSelectedJointIndex(int index)
    {
        if (articulationChain.Length > 0) 
        {
            // Skips joints that cannot move.
            if (index < 2) index = 7;
            else if (index > 7) index = 2;
            selectedIndex = (index + articulationChain.Length) % articulationChain.Length;
        }
    }

    void Update()
    {
        bool SelectionInput1 = Input.GetKeyDown("i");
        bool SelectionInput2 = Input.GetKeyDown("k");

        SetSelectedJointIndex(selectedIndex); // to make sure it is in the valid range
        UpdateDirection(selectedIndex);

        if (SelectionInput2)
        {
            SetSelectedJointIndex(selectedIndex - 1);
            Highlight(selectedIndex);
        }
        else if (SelectionInput1)
        {
            SetSelectedJointIndex(selectedIndex + 1);
            Highlight(selectedIndex);
        }

        UpdateDirection(selectedIndex);

        // TEST
        /*
        counter++;
        if (counter >= 100)
        {
            List<float> positions = new List<float>();
            float[] positionArr = { 20.0f, 210.0f, 59.0f, 100.0f, 90.0f, 24.0f };
            positions.AddRange(positionArr);
            SetJointPositions(positions);
            counter = 0;
        }
        */
    }

    /// <summary>
    /// Highlights the color of the robot by changing the color of the part to a color set by the user in the inspector window
    /// </summary>
    /// <param name="selectedIndex">Index of the link selected in the Articulation Chain</param>
    private void Highlight(int selectedIndex)
    {
        if (selectedIndex == previousIndex || selectedIndex < 0 || selectedIndex >= articulationChain.Length) 
        {
            return;
        }

        // reset colors for the previously selected joint
        ResetJointColors(previousIndex);

        // store colors for the current selected joint
        StoreJointColors(selectedIndex);

        DisplaySelectedJoint(selectedIndex);
        Renderer[] rendererList = articulationChain[selectedIndex].transform.GetChild(0).GetComponentsInChildren<Renderer>();

        // set the color of the selected join meshes to the highlight color
        foreach (var mesh in rendererList)
        {
            MaterialExtensions.SetMaterialColor(mesh.material, highLightColor);
        }
    }

    void DisplaySelectedJoint(int selectedIndex)
    {
        if (selectedIndex < 0 || selectedIndex >= articulationChain.Length) 
        {
            return;
        }
        selectedJoint = articulationChain[selectedIndex].name + " (" + selectedIndex + ")";
    }

    /// <summary>
    /// Sets the direction of movement of the joint on every update
    /// </summary>
    /// <param name="jointIndex">Index of the link selected in the Articulation Chain</param>
    private void UpdateDirection(int jointIndex)
    {
        if (jointIndex < 0 || jointIndex >= articulationChain.Length) 
        {
            return;
        }

        //float moveDirection = Input.GetAxis("Vertical");

        JointControl current = articulationChain[jointIndex].GetComponent<JointControl>();
        
        if (previousIndex != jointIndex)
        {
            JointControl previous = articulationChain[previousIndex].GetComponent<JointControl>();
            previous.direction = RotationDirection.None;
            previousIndex = jointIndex;
        }

        if (current.controltype != control) 
        {
            UpdateControlType(current);
        }
        
        // Rewritten to use keys instead of axis movement.
        if (!movingPositive && Input.GetKeyDown("l"))
        {
            current.direction = RotationDirection.Positive;
            movingPositive = true;
            movingNegative = false;
            coroutine = move("l");
            StartCoroutine(coroutine);
        }

        else if (!movingNegative && Input.GetKeyDown("j"))
        {
            current.direction = RotationDirection.Negative;
            movingNegative = true;
            movingPositive = false;
            coroutine = move("j");
            StartCoroutine(coroutine);
        } else if (!movingPositive && !movingNegative) {
            current.direction = RotationDirection.None;
        }
        
    }

    private IEnumerator move(string key)
    {
        while (!Input.GetKeyUp(key))
        {
            yield return null;
        }
        if (key.Equals("l")) movingPositive = false;
        else if (key.Equals("j")) movingNegative = false;
    }

    /// <summary>
    /// Stores original color of the part being highlighted
    /// </summary>
    /// <param name="index">Index of the part in the Articulation chain</param>
    private void StoreJointColors(int index)
    {
        Renderer[] materialLists = articulationChain[index].transform.GetChild(0).GetComponentsInChildren<Renderer>();
        prevColor = new Color[materialLists.Length];
        for (int counter = 0; counter < materialLists.Length; counter++)
        {
            prevColor[counter] = MaterialExtensions.GetMaterialColor(materialLists[counter]);
        }
    }

    /// <summary>
    /// Resets original color of the part being highlighted
    /// </summary>
    /// <param name="index">Index of the part in the Articulation chain</param>
    private void ResetJointColors(int index)
    {
        Renderer[] previousRendererList = articulationChain[index].transform.GetChild(0).GetComponentsInChildren<Renderer>();
        for (int counter = 0; counter < previousRendererList.Length; counter++)
        {
            MaterialExtensions.SetMaterialColor(previousRendererList[counter].material, prevColor[counter]);
        }
    }

    public new void OnGUI()
    {
        GUIStyle centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        GUI.Label(new Rect(Screen.width / 2 - 200, 10, 400, 20), "Press I/K arrow keys to select a robot joint.", centeredStyle);
        GUI.Label(new Rect(Screen.width / 2 - 200, 30, 400, 20), "Press J/L arrow keys to move " + selectedJoint + ".", centeredStyle);
    }


    /*
    public void SetJointPositions(List<float> positions)
    {
        GameObject baseObject = transform.GetChild(1).GetChild(3).gameObject;
        baseObject.GetComponent<ArticulationBody>().SetDriveTargets(positions);
        Debug.Log("Setting joint positions");
        /*
        for (int i = 0; i < positions.Length; i++)
        {
            ArticulationBody current = articulationChain[i + 2];
            current.xDrive.target = positions[i];
            Debug.Log("setting position of joint " + (i + 1));

        }
        */
    }
    */
}
