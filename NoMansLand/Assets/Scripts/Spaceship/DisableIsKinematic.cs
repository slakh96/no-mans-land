using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace; 

public class DisableIsKinematic : MonoBehaviour
{
	private float elapsedTime;
	
	public GameObject winscreen; 
	public GameObject goscreen;
	public GameObject healthBars;
	public GameObject compass;
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
	    SpaceshipManager.DoSpaceshipSetup(spaceship);
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
			MainMenuScript.ToWinScreen();
			// winscreen.SetActive(true);
			// healthBars.SetActive(false);
			// compass.SetActive(false);
		}
		else if (SpaceshipManager.SpaceshipDestroyed())
		{
			MainMenuScript.ToGameOver();
			// goscreen.SetActive(true);
			// healthBars.SetActive(false);
			// compass.SetActive(false);
		}
		elapsedTime += Time.deltaTime;
    }
}
