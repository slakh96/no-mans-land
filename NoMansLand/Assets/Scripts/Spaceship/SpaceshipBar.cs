using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipBar : MonoBehaviour
{
    public Slider slider;
    public Image sliderFill;
    private void Start()
    {
        slider.maxValue = 100f;
        slider.value = SpaceshipManager.GetSpaceshipHealth();
    }
    
    private void setHealth(int health)
    {
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        setHealth(Mathf.RoundToInt(SpaceshipManager.GetSpaceshipHealth() * 100f));
        Color currentColor = Color.Lerp(Color.red, Color.green, SpaceshipManager.GetSpaceshipHealth());
        currentColor.a = 0.5f;
        sliderFill.color = currentColor;
    }
    
}