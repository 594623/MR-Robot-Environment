using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetOpener : MonoBehaviour
{
    public GameObject preset1;
    public GameObject preset2;
    public GameObject preset3;
    public GameObject preset4;
    public GameObject preset5;
    public GameObject preset6;
    public GameObject preset7;
    public GameObject preset8;

    public void Togglepreset1()
    {
        preset1.SetActive(!preset1.activeSelf);
    }
    public void Togglepreset2()
    {
        preset2.SetActive(!preset2.activeSelf);
    }
    public void Togglepreset3()
    {
        preset3.SetActive(!preset3.activeSelf);
    }
    public void Togglepreset4()
    {
        preset4.SetActive(!preset4.activeSelf);
    }
    public void Togglepreset5()
    {
        preset5.SetActive(!preset5.activeSelf);
    }
    public void Togglepreset6()
    {
        preset6.SetActive(!preset6.activeSelf);
    }
    public void Togglepreset7()
    {
        preset7.SetActive(!preset7.activeSelf);
    }
    public void Togglepreset8()
    {
        preset8.SetActive(!preset8.activeSelf);
    }
}

