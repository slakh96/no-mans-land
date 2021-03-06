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
	
	// The number of time bonuses given so far
	private int numBonuses = 0;
    // Start is called before the first frame update
    void Start()
    {
	    if (SpaceshipManager.GetIsStarting())
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
		}
		else if (SpaceshipManager.SpaceshipDestroyed())
		{
			MainMenuScript.ToGameOver();
		}

		//Over time, give the player more of a bonus as they will have to scavenge further for pieces
		// Check for one arbitrary name to prevent this if block from getting executed for every part that has disableIsKinematic
		if (elapsedTime > (30 * numBonuses) && gameObject.name == "engine_lft_geo")
		{
			SpaceshipManager.IncreaseDropoffBonus();
			numBonuses += 1;
		}
		elapsedTime += Time.deltaTime;
    }
}
