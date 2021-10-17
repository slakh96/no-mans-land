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
	    GameObject spaceship = GameObject.Find("spaceShip 1 2 1");
	    SpaceshipManager.SetOriginalPartData(spaceship);
    }

    // Update is called once per frame
    void Update()
    {
		if (elapsedTime > SpaceshipManager.GetCrumbleTime(gameObject.name) && !SpaceshipManager.IsDropped(gameObject.name))
        {
	        SpaceshipManager.DropPartFromShip(gameObject.name);
        }

		if (elapsedTime > 11 && elapsedTime < 13)
		{
			SpaceshipManager.AddPartToShip();
		}
		elapsedTime += Time.deltaTime;
    }
}
