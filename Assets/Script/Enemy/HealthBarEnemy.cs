using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider slider;
    public Image fillImage;
    public void Initialize(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateHealthBar(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
