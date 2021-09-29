using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenMeter : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private Image uiFill;
    [SerializeField] private Text oxygenPercentage;

    private float time = 100f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0f)
        {
            time -= Time.deltaTime;
            uiFill.fillAmount = Mathf.InverseLerp(0, 100f, time);
        }
        else
        {
            Destroy(player);
        }
        oxygenPercentage.text = Mathf.Round(time) + "%";
    }
}
