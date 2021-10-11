using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace; 

public class DisableIsKinematic : MonoBehaviour
{
	public GameObject obj;
	public float baseTime;
	private float elapsedTime;

	// A multiplier to control how long initially it takes for the ship to completely crumble, in units of 53s.
	// E.g. timeMultiplier == 1.0 => 1 * 53s until spaceship finishes crumbling.
	// timeMultiplier == 5.0 => 5 * 53s = 265s = 4min25s until it finishes crumbling. 
	private float baseTimeMultiplier = 5.0f;

	private float adjustedBaseTime;
    // Start is called before the first frame update
    void Start()
    {
	    elapsedTime = 0;
	    adjustedBaseTime = baseTimeMultiplier * baseTime;
	    Debug.Log(obj.name); 
	    Debug.Log((object)obj.name.Equals("polySurface6")); 
    }

    // Update is called once per frame
    void Update()
    {
		if (elapsedTime > adjustedBaseTime + SpaceshipTimekeeping.BonusTime)
        {
        	Rigidbody cubeRigidbody = GetComponent<Rigidbody>();
        	cubeRigidbody.isKinematic = false;
        }
		elapsedTime += Time.deltaTime;
    }
}
