using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject place;
    public GameObject delete;
    public GameObject sliderHeight;
    public GameObject sliderWidth;
    public GameObject sliderLength;
    public GameObject robotHeigth;
    public GameObject robotDistanceofset;
    public GameObject tableHeigth;
    public GameObject presets;
    public GameObject imageTarget;

    public void PlaceMenu()
    {
        place.SetActive(!place.activeSelf);
    }

    public void DeleteMenu()
    {
        delete.SetActive(!delete.activeSelf);
    }

    public void SliderHeigth()
    {
        sliderHeight.SetActive(!sliderHeight.activeSelf);
    }

    public void SliderLength()
    {
        sliderLength.SetActive(!sliderLength.activeSelf);
    }

    public void SliderWidth()
    {
        sliderWidth.SetActive(!sliderWidth.activeSelf);
    }

    public void TableHeigth()
    {
        tableHeigth.SetActive(!tableHeigth.activeSelf);
    }

    public void RobotHeigth()
    {
        robotHeigth.SetActive(!robotHeigth.activeSelf);
    }

    public void RobotDistance()
    {
        robotDistanceofset.SetActive(!robotDistanceofset.activeSelf);
    }

    public void PresetsMenu()
    {
        presets.SetActive(!presets.activeSelf);
    }

    public void Imagetarget()
    {
        imageTarget.SetActive(!presets.activeSelf);
    }
}
