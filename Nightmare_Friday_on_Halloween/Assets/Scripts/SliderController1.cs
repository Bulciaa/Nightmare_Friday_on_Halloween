using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController1 : MonoBehaviour
{
 public Slider slider1;

    public void OnSliderValueChanged()
    {
        float value = slider1.value;
        PlayerPrefs.SetFloat("SliderValue", value);
	PlayerPrefs.Save();
    }
}
