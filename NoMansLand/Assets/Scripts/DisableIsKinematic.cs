using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace; 

public class DisableIsKinematic : MonoBehaviour
{
	private float elapsedTime;
	
	public GameObject winscreen; 
	public GameObject goscreen;
    // Start is called before the first frame update
    void Start()
    {
	    elapsedTime = 0;
	    GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
	    if (spaceship == null)
	    {
		    Debug.Log("Spaceship is NULL; probably forgot the tag on the spaceship");
		    return;
	    }
	    SpaceshipManager.SetOriginalPartData(spaceship);
    }

    // Update is called once per frame
    void Update()
    {
		if (elapsedTime >= SpaceshipManager.GetCrumbleTime(gameObject.name) && !SpaceshipManager.IsDropped(gameObject.name))
        {
	        SpaceshipManager.DropPartFromShip(gameObject.name);
        }

		if (SpaceshipManager.SpaceshipComplete())
		{
			winscreen.SetActive(true);
		}
		else if (SpaceshipManager.SpaceshipDestroyed())
		{
			goscreen.SetActive(true);
		}
		elapsedTime += Time.deltaTime;
    }
}
