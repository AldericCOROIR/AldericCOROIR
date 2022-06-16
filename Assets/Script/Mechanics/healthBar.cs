using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image Health;


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        Health.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        Health.color = gradient.Evaluate(slider.normalizedValue);
    }


}
