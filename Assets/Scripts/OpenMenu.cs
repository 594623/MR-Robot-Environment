using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    // TODO: Use ButtonMethods.OpenMenu instead.
    public void open(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
