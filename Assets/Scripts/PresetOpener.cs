using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetOpener : MonoBehaviour
{
    public GameObject[] presets;

    public void TogglePreset(int number)
    {
        for (int i = 0; i < presets.Length; i++)
        {
            presets[i].SetActive(false);
        }
        presets[number].SetActive(true);
    }
}

