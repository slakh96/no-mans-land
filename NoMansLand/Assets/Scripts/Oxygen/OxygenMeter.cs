using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    public Slider slider;
    public Image sliderFill;
    private GameObject player;
    private CharacterControllerMovement currPlayerControls;
    private bool isPlaying;

    public float maxTime = 150f;

    private float time;
    private float currPercentage;
    
    // Gameover 
    public GameObject goscreen;
    public GameObject healthbars;
    public GameObject compass;

    private void Start()
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
        time = maxTime;
        player = GameObject.FindWithTag("Player");
        currPlayerControls = player.GetComponent<CharacterControllerMovement>();
    }
    
    private void setHealth(int health)
    {
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currPlayerControls.replenishHealth)
        {
            time += 15f / 100f * maxTime;
            if (time >= maxTime)
            {
                time = maxTime;
            }
            currPlayerControls.replenishHealth = false;
        }
        currPercentage = slider.value / maxTime * 100f;
        if (time >= 0f)
        {
            time -= Time.deltaTime;

            // Interval and timing changes based on how long the Oxygen Meter is. 
            if (currPercentage <= 40f && currPercentage > 20f && !isPlaying)
            {
                StartCoroutine (playSound("Breathing1", 20));
            }
            if (currPercentage <= 20f && currPercentage > 10f && !isPlaying)
            {
                StartCoroutine (playSound("Breathing2", 10));
            }
            if (currPercentage <= 10f && !isPlaying)
            {
                StartCoroutine (playSound("Breathing3", 20));
            }
        }
        else
        {
            Destroy(player);
            goscreen.SetActive(true);
            compass.SetActive(false);
            healthbars.SetActive(false);
        }
        Color currentColor = Color.Lerp(Color.red, Color.green, slider.value / maxTime);
        currentColor.a = 0.5f;
        sliderFill.color = currentColor;
        setHealth(Mathf.RoundToInt(time));
    }
    
    IEnumerator playSound(string name, int interval)
    {
        isPlaying = true;
        Sound s = FindObjectOfType<AudioManager>().Find(name);
        s.source.Play();
        yield return new WaitForSeconds (interval);
        s.source.Stop();
        isPlaying = false;
    }
}