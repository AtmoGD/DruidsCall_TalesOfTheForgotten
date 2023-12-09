using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public HealthComponent HealthComponent;
    public Slider HealthSlider;

    private void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        HealthSlider.value = (float)HealthComponent.CurrentHealth / (float)HealthComponent.MaxHealth;
    }
}
