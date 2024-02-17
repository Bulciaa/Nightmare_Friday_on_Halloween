using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIScript : MonoBehaviour
{
    public TMP_Text numberText;
    private int number = 0;

    void Start()
    {
        InvokeRepeating("IncreaseNumberValue", 1f, 0.5f);
    }

    void IncreaseNumberValue()
    {
        number++;
        numberText.text = number.ToString();
    }
}
