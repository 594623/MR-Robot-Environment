using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotTeleporter : MonoBehaviour
{
    GameObject imageTarget;
    GameObject baseObject;
    bool isTracked;

    float x_offset = 0.0f;
    float y_offset = 0.0f;
    float z_offset = -0.25f;

    // Start is called before the first frame update
    void Start()
    {
        imageTarget = GameObject.Find("ImageTarget");
        baseObject = transform.GetChild(1).GetChild(3).gameObject;
        isTracked = false;
    }

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
        Vector3 pos = gObject.transform.position;
        pos.Set(pos.x + x_offset, pos.y + y_offset, pos.z + z_offset);
        baseObject.GetComponent<ArticulationBody>().TeleportRoot(pos, gObject.transform.rotation);
    }
}
