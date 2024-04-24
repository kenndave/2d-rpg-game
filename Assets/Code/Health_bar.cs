using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_bar : MonoBehaviour
{
    public Slider slider;
    public void sethealth(float hp)
    {
        slider.value = hp;
    }
}
