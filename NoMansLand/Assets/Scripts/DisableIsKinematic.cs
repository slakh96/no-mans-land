using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIsKinematic : MonoBehaviour
{
	public GameObject cube;
	public float time;

	// A multiplier to control how long initially it takes for the ship to completely crumble, in units of 53s.
	// E.g. timeMultiplier == 1.0 => 1 * 53s until spaceship finishes crumbling.
	// timeMultiplier == 5.0 => 5 * 53s = 265s = 4min25s until it finishes crumbling. 
	private float timeMultiplier = 5.0f;

	private float adjustedTime;
    // Start is called before the first frame update
    void Start()
    {
	    adjustedTime = timeMultiplier * time;
    }

    // Update is called once per frame
    void Update()
    {
		if (adjustedTime <= 0f)
        {
        	Rigidbody cubeRigidbody = GetComponent<Rigidbody>();
        	cubeRigidbody.isKinematic = false;
        }
		adjustedTime -= Time.deltaTime;
    }
}
