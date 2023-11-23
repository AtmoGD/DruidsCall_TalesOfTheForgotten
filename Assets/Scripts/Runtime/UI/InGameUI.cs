using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public HealthComponent Hero;
    public Slider HealthSlider;

    private void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        HealthSlider.value = Hero.CurrentHealth / Hero.MaxHealth;
    }
}
