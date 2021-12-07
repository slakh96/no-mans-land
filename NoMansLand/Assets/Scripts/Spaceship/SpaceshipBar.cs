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
    private float time;
    private void Start()
    {
        slider.maxValue = 100f;
        time = 0;
        slider.value = SpaceshipManager.GetSpaceshipHealth();
    }
    
    private void setHealth(int health)
    {
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        setHealth(Mathf.RoundToInt(SpaceshipManager.GetSpaceshipHealth() * 100f));
        Color currentColor = Color.Lerp(Color.red, Color.green, SpaceshipManager.GetSpaceshipHealth());
        currentColor.a = 0.5f;
        if (Mathf.RoundToInt(SpaceshipManager.GetSpaceshipHealth() * 100f) <= 25f)
        {
            if ((int) time % 4 == 0)
            {
                currentColor.a = 0.99f;
            }
            else if ((int) time % 4 == 1)
            {
                currentColor.a = 0.5f;
            }
            else if ((int) time % 4 == 2)
            {
                currentColor.a = 0.99f;
            }
            else if ((int) time % 4 == 3)
            {
                currentColor.a = 0.5f;
            }
        }
        sliderFill.color = currentColor;
    }
    
}