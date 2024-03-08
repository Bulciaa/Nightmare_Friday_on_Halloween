using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController2 : MonoBehaviour
{
    public Slider slider2;

    private void Start()
    {
        float value = PlayerPrefs.GetFloat("SliderValue");
        slider2.value = value;
    }
}
