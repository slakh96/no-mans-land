using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace; 

public class DisableIsKinematic : MonoBehaviour
{
	private float elapsedTime;
	
    // Start is called before the first frame update
    void Start()
    {
	    elapsedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (elapsedTime > SpaceshipTimekeeping.GetCrumbleTime(gameObject.name))
        {
        	Rigidbody cubeRigidbody = GetComponent<Rigidbody>();
        	cubeRigidbody.isKinematic = false;
        }
		elapsedTime += Time.deltaTime;
    }
}
