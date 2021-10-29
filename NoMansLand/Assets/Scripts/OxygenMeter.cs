using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    private GameObject player;
    private CharacterControllerMovement currPlayerControls;
    [SerializeField] private Image uiFill;
    [SerializeField] private Text oxygenPercentage;
    public GameObject goscreen; 
    private bool isPlaying;

    public float maxTime = 200f;

    public float time = 200f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        currPlayerControls = player.GetComponent<CharacterControllerMovement>();
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
        float currPercentage = time / maxTime * 100f;
        if (time >= 0f)
        {
            time -= Time.deltaTime;
            uiFill.fillAmount = Mathf.InverseLerp(0, maxTime, time);

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
            this.gameObject.SetActive(false);
        }
        
        oxygenPercentage.text = Mathf.Round(currPercentage) + "%";
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