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
	    GameObject spaceship = GameObject.FindGameObjectWithTag("Spaceship");
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
			Debug.Log("GAME OVER PLAYER WINS ========================================================");
		}
		else if (SpaceshipManager.SpaceshipDestroyed())
		{
			Debug.Log("GAME OVER PLAYER LOST ========================================================");
		}
		elapsedTime += Time.deltaTime;
    }
}
