using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public void open(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
