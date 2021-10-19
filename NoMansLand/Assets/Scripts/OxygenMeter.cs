using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Image uiFill;
    [SerializeField] private Text oxygenPercentage;
    public GameObject goscreen; 
    private bool isPlaying;

    public float time = 100f;

    // Update is called once per frame
    void Update()
    {
        if (time >= 0f)
        {
            time -= Time.deltaTime;
            uiFill.fillAmount = Mathf.InverseLerp(0, 100f, time);
            
            // Interval and timing changes based on how long the Oxygen Meter is. 
            if (time <= 50f && time > 30f && !isPlaying)
            {
                StartCoroutine (playSound("Breathing1", 20));
            }
            if (time <= 30f && time > 20f && !isPlaying)
            {
                StartCoroutine (playSound("Breathing2", 10));
            }
            if (time <= 20f && !isPlaying)
            {
                StartCoroutine (playSound("Breathing3", 20));
            }
        }
        else
        {
            Destroy(player);
            goscreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
        oxygenPercentage.text = Mathf.Round(time) + "%";
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
