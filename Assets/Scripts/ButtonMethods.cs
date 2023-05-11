using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonMethods : MonoBehaviour
{
    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void LockEnvironment(GameObject imageTarget)
    {
        ImageTargetBehaviour script = imageTarget.GetComponent<ImageTargetBehaviour>();
        script.enabled = !script.enabled;
    }
}
