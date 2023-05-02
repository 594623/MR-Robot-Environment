using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTeleporter : MonoBehaviour
{
    public GameObject imageTarget;
    public GameObject baseObject;

    private bool isTracked = false;

    // Update is called once per frame
    void Update()
    {
        if (isTracked)
        {
            TeleportRobot(imageTarget);
        }
    }

    public void enableTeleport()
    {
        isTracked = true;
    }

    public void disableTeleport()
    {
        isTracked= false;
    }

    public void TeleportRobot(GameObject gObject)
    {
        baseObject.GetComponent<ArticulationBody>().TeleportRoot(gObject.transform.position, gObject.transform.rotation);
    }
}
